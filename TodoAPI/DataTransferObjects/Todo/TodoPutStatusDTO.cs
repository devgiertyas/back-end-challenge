using System.ComponentModel.DataAnnotations;
using static TodoAPI.Helpers.EnumApp;

namespace TodoAPI.DataTransferObjects.Todo
{
    public class TodoPutStatusDTO
    {
        [Required]
        public StatusTodo Status { get; set; }
    }
}
