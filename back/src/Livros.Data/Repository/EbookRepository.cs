using Dapper;
using Livros.Data.Data;
using Livros.Data.Interface;
using Livros.Domain.Models;
using Microsoft.Extensions.Configuration;

namespace Livros.Data.Repository
{
    public class EbookRepository : BaseRepository, IEbookRepository
    {
        public EbookRepository(IConfiguration configuration) : base(configuration)
        {
        }

        ~EbookRepository()
        {
            Dispose();
        }

        public async Task<IEnumerable<Ebook>> ListEbooks()
        {
            var connection = GetConnection();

            try
            {
                var result = await connection.QueryAsync<Ebook>("SELECT * FROM Ebook");
                
                return result;
            }
            catch (System.Exception ex)
            {
                
                throw new Exception($"Error: {ex.Message}");
            }
            
        }
        
        public async Task<Ebook> GetById(int id)
        {
            var connection = GetConnection();
            
            try
            {
                var result = await connection.QueryFirstAsync<Ebook>($"SELECT * FROM Ebook WHERE id = {id}");
                
                return result;
            }
            catch (System.Exception ex)
            {
                
                throw new Exception($"Error: {ex.Message}");
            }
        }
    }
}