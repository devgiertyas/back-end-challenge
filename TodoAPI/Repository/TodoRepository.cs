using Microsoft.EntityFrameworkCore;
using TodoAPI.Context;
using TodoAPI.Models;
using TodoAPI.Repository.Interfaces;
using static TodoAPI.Helpers.EnumApp;

namespace TodoAPI.Repository
{
    public class TodoRepository : BaseRepository, ITodoRepository
    {

        private readonly ContextDatabase _context;

        public TodoRepository(ContextDatabase context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Todo>> GetTodoAsync()
        {
            return await _context.Todos.Include(x => x.TodoUsers).ThenInclude(ot => ot.User)
                .ToListAsync();
        }

        public async Task<Todo> GetTodoByIdAsync(int id)
        {
            return await _context.Todos.Include(x => x.TodoUsers).ThenInclude(ot => ot.User).Where(x => x.Id == id).FirstOrDefaultAsync();

        }

        public async Task<TodoUser> GetTodoUser(int todoId, int userId)
        {
            return await _context.TodoUser
                .Where(x => x.UserId == userId && x.TodoId == todoId)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Todo>> GetTodoByStatus(StatusTodo status)
        {
            return await _context.Todos.Where(x => x.Status == status).ToListAsync();
        }


}
}
