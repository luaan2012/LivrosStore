using Livros.Data.Interface;
using Livros.Domain.Models;
using Livros.Service.Interface;
using Microsoft.Extensions.Configuration;
using static Livros.Helper.EmailLog.LogHelper;

namespace Livros.Service.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
        }
        public async Task CreateUser(User user)
        {
            await _userRepository.CreateUser(user);
        }

        public async Task DeleteAccount(int id)
        {
            await _userRepository.DeleteAccount(id);
        }

        public async Task<User> GetUser(string email, string password)
        {
            return await _userRepository.GetUser(email, password);
        }

        public async Task RecoveryPassword(string email, EmailLog emailLog)
        {
            var result = await _userRepository.RecoveryPassword(email);

            if(result is not null)
            {
                emailLog.To = email;
                SendEmail(emailLog, result.Id);
            }
        }

        public async Task ChangePassword(int id, string password)
        {
            await _userRepository.ChangePassword(id, password);
        }

        public async Task UpdateAccount(User user)
        {
            await _userRepository.UpdateAccount(user);
        }

        public async Task<User> GetUserByName(string name)
        {
            return await _userRepository.GetUserByName(name);
        }
    }
}