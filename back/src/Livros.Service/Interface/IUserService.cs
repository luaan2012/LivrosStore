using Livros.Domain.Models;
using static Livros.Helper.EmailLog.LogHelper;

namespace Livros.Service.Interface
{
    public interface IUserService
    {
        Task CreateUser(User user);
        Task RecoveryPassword(string email, EmailLog emailLog);
        Task DeleteAccount(int id);
        Task UpdateAccount(User user);
        Task<User> GetUser(string email, string password);
        Task ChangePassword(int id, string password);
        Task<User> GetUserByName(string name);
    }
}