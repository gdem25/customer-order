using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using CustomerOrderModel.Models;
using CustomerOrderBack.Dtos;
using CustomerOrderBack;

namespace CustomerOrderBack.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly CustomerOrdersContext _context;
        private readonly UserManager<CustomerOrderUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtHandler _jwtHandler;

        public AccountController(CustomerOrdersContext context, UserManager<CustomerOrderUser> userManager, RoleManager<IdentityRole> roleManager,  JwtHandler jwtHandler)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtHandler = jwtHandler;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            CustomerOrderUser? user = await _userManager.FindByNameAsync(loginRequest.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginRequest.Password))
            {
                return Unauthorized(new LoginResponse
                {
                    Success = false,
                    Message = "Invalid Username or Password."
                });
            }

            JwtSecurityToken secToken = await _jwtHandler.GetTokenAsync(user);
            string? jwt = new JwtSecurityTokenHandler().WriteToken(secToken);
            return Ok(new LoginResponse
            {
                Success = true,
                Message = "Login successful",
                Token = jwt
            });
        }

        [HttpPost("Signup")]
        public async Task<ActionResult<LoginResponse>> SignUp(SignupRequest userRequest)
        {
            const string roleUser = "RegisteredUser";

            int count = 0;

            string jwt = "";

            if (await _roleManager.FindByNameAsync(roleUser) is null)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleUser));
            }

            if ( await _userManager.FindByNameAsync(userRequest.UserName) is null)
            {
                
                CustomerOrderUser user = new()
                {
                    UserName = userRequest.UserName,
                    Email = userRequest.Email,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                await _userManager.CreateAsync(user, userRequest.Password);
                await _userManager.AddToRoleAsync(user, roleUser);
                user.EmailConfirmed= true;
                user.LockoutEnabled= false;
                JwtSecurityToken secToken = await _jwtHandler.GetTokenAsync(user);
                jwt = new JwtSecurityTokenHandler().WriteToken(secToken);
                count++;
            }

            if(count <= 0)
            {
                return BadRequest();
            }

            await _context.SaveChangesAsync();
            return Ok(new LoginResponse
            {
                Success= true,
                Message= "Sign up was successfull",
                Token= jwt
            });
        }

    }
}