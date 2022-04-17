using System.ComponentModel.DataAnnotations;

namespace TodoAPI.DataTransferObjects.User
{
    public class UserAddTDO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
