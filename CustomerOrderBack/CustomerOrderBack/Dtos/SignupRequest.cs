using System.ComponentModel.DataAnnotations;

namespace CustomerOrderBack.Dtos
{
    public class SignupRequest
    {
        [Required(ErrorMessage = "username required")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "email required")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "password required")]
        public string Password { get; set; } = null!;
    }
}
