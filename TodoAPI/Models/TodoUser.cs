namespace TodoAPI.Models
{
    public class TodoUser : Base
    {
        public int TodoId { get; set; }
        public Todo Todo{ get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
