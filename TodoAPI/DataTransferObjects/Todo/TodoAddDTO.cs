using System.ComponentModel.DataAnnotations;
using static TodoAPI.Helpers.EnumApp;

namespace TodoAPI.DataTransferObjects.Todo
{
    public class TodoAddDTO
    {
        [Required]
        public string Category { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Project { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime ExpectedDate { get; set; }

        [Required]
        [EnumDataType(typeof(StatusTodo))]
        public StatusTodo Status { get; set; }

        public int Sequence { get; set; }

        [Required]
        public string Time { get; set; }
    }
}
