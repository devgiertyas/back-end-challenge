using System.ComponentModel.DataAnnotations;

namespace TodoAPI.DataObjects
{
    public class LoginDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
