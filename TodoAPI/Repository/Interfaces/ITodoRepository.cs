using TodoAPI.Models;

namespace TodoAPI.Repository.Interfaces
{
    public interface ITodoRepository : IBaseRepository
    {

        Task<Todo> GetTodoByIdAsync(int id);

    }
}
