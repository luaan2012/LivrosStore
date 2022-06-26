using Livros.Domain.Models;

namespace Livros.Data.Interface
{
    public interface IUserRepository
    {
        Task CreateUser(User user);
        Task<User> RecoveryPassword(string email);
        Task DeleteAccount(int id);
        Task UpdateAccount(User user);        
        Task<User> GetUser(string email, string password);
        Task ChangePassword(int id, string password);
        Task<User> GetUserByName(string name);
    }
}