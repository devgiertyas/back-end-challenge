using System.ComponentModel.DataAnnotations;

namespace TodoAPI.DataObjects
{
    public class UserDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
        public DateTime? CreatedDate { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
