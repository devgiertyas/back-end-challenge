namespace TodoAPI.Models
{

    public class User : Base
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Password { get; set; }
        public ICollection<TodoUser> TodoUsers { get; set; }
    }
}
