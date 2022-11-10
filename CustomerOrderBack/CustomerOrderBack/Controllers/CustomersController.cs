using CustomerOrderBack.Dtos;
using CustomerOrderModel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerOrderBack.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerOrdersContext _context;
        public CustomersController(CustomerOrdersContext context)
        {
            _context = context;
        }
        // GET: CustomersController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerOrder>> GetCustomer(int id)
        {
            CustomerOrder? customerDto = await _context.Customers
                .Where(x => x.Id == id)
                .Select(x => new CustomerOrder
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Orders = x.Orders
                }).SingleOrDefaultAsync();
            if (customerDto == null) return NotFound();
            return Ok(customerDto);
        }
 
        [HttpGet("Orders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }
    }
}
