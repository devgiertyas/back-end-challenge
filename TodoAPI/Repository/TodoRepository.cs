using Microsoft.EntityFrameworkCore;
using TodoAPI.Context;
using TodoAPI.DataObjects;
using TodoAPI.Models;
using TodoAPI.Repository.Interfaces;

namespace TodoAPI.Repository
{
    public class TodoRepository : BaseRepository, ITodoRepository
    {

        private readonly ContextDatabase _context;

        public TodoRepository(ContextDatabase context) : base(context)
        {
            _context = context;
        }

        public async Task<Todo> GetTodoByIdAsync(int id)
        {
            return await _context.Todos.FindAsync(id);

        }
    }
}
