using TodoAPI.Models;
using static TodoAPI.Helpers.EnumApp;

namespace TodoAPI.Repository.Interfaces
{
    public interface ITodoRepository : IBaseRepository
    {

        Task<Todo> GetTodoByIdAsync(int id);

        Task<TodoUser> GetTodoUser(int todoId, int userId);

        Task<List<Todo>> GetTodoByStatus(StatusTodo status);

        Task<IEnumerable<Todo>> GetTodoAsync();

    }
}
