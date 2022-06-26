using Livros.Data.Data;
using Livros.Data.Interface;
using Livros.Domain.Models;
using Microsoft.Extensions.Configuration;
using Dapper;
using Livros.Helper.EmailLog;


namespace Livros.Data.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        {
        }

        ~UserRepository()
        {
            Dispose();
        }

        public async Task CreateUser(User user)
        {
            var connection = GetConnection();

            try
            {
                await connection.QueryAsync($@"INSERT INTO User (Name, LastName, Address, Number, Password, ClientType, Email) VALUES (
                    '{user.Name}', '{user.LastName}', '{user.Address}', '{user.Number}', '{user.Password}', '{user.ClientType}', '{user.Email}')");

            }
            catch (System.Exception ex)
            {
                
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task DeleteAccount(int id)
        {
            var connection = GetConnection();

            try
            {
                await connection.QueryAsync($"DELETE FROM User where Id = '{id}'");
            }
            catch (System.Exception ex)
            {
                
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<User> GetUser(string email, string password)
        {
            var connection = GetConnection();
            try
            {
                var result = await connection.QueryFirstAsync<User>($"SELECT * FROM User where Email = '{email}' AND Password = '{password}'");

                if(result is not null)
                    return result;   

                throw new Exception();

            }
            catch (System.Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<User> RecoveryPassword(string email)
        {
            var connection = GetConnection();
            
            try
            {
                var result = await connection.QueryFirstAsync<User>($"SELECT * FROM User WHERE Email = '{email}'");   

                if(result is not null)
                    return result;
                else
                    throw new Exception();

            }
            catch (System.Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task ChangePassword(int id, string password)
        {
            var connection = GetConnection();
            
            try
            {
                var result = await connection.ExecuteAsync($"UPDATE User SET Password = '{password}' WHERE id = '{id}'");   
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task UpdateAccount(User user)
        {
            var connection = GetConnection();
            
            try
            { 
                var result = connection.QueryFirstAsync($"SELECT * FROM User where Id = '{user.Id}'");

                if(result.Result is not null)
                    await connection.QueryAsync($@"UPDATE User SET Name = '{user.Name}', LastName = '{user.LastName}', Address = '{user.Address}',
                        Number = '{user.Number}' where Id = {user.Id}");    

            }
            catch (System.Exception ex)
            {
                
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<User> GetUserByName(string name)
        {
            var connection = GetConnection();

            try
            {
                if(name == "null")
                    return null;

                var result = await connection.QueryFirstAsync<User>($"SELECT * From user where Name = '{name}'");

                if(result is not null)
                    return result;
                else
                    throw new Exception();
            }
            catch (System.Exception ex)
            {                
                throw new Exception($"Erro: {ex.Message}");
            }
        }
    }
}