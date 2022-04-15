namespace TodoAPI.Models
{
    public class Todo : Base
    {
   
        public string Category { get; set; }

        public string Title{ get; set; }

        public string Project{ get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ExpectedDate { get; set; }

        public int Status { get; set; }

        public int Sequence { get; set; }
        public string Time { get; set; }

    }
}
