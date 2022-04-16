using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.Context;
using TodoAPI.Models;
using TodoAPI.Repository.Interfaces;

namespace TodoAPI.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {

        private readonly ContextDatabase _context;

        public UserRepository(ContextDatabase context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {

            return await _context.Users
            .Where(x => x.Id == 1)
            .FirstOrDefaultAsync();
        }

        public async Task<bool> CheckUserByEmailAsync(string email)
        {

            User user =  await _context.Users
            .Where(x => x.Email.Equals(email))
            .FirstOrDefaultAsync();

            if (user == null)
                return false;
            else
            {
                return true;
            }
        }

        public async Task<User>GetUserByEmailAndPasswordAsync(string email, string password)
        {
            return await _context.Users
            .Where(x => x.Email.Equals(email) && x.Password == password)
            .FirstOrDefaultAsync();
        }

    }
}
