using System.ComponentModel.DataAnnotations;

namespace TodoAPI.DataTransferObjects.Todo
{
    public class TodoPutSequenceDTO
    {
        [Required]
        public int Sequence { get; set; }
    }
}
