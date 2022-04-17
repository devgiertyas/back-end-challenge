using TodoAPI.Models;

namespace TodoAPI.Repository.Interfaces
{
    public interface IUserRepository : IBaseRepository
    {

        Task<User> GetUserByIdAsync(int id);

        Task<User> GetUserByEmailAsync(string email);

        Task<bool> CheckUserByEmailAsync(string email);

        Task<User> GetUserByEmailAndPasswordAsync(string email, string password);

        Task<IEnumerable<User>> GetUserAsync();


    }
}
