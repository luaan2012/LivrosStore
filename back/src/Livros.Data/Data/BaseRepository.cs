using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using TudoFarmaRep.Model.Models;

namespace Livros.Data.Data
{
    public class BaseRepository : IDisposable
    {
        private readonly string _connectionString;
        private readonly bool _multipleSqlConnection;
        private static SqliteConnection _sqlConnection;
        private IConfiguration _configuration;
        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("default");
            _multipleSqlConnection = configuration["MultipleSqlConnection"].EndsWith(bool.TrueString);
            _configuration = configuration;
        }
        private SqliteConnection BuildConnection()
        {
            return new SqliteConnection(_connectionString);
        }
        protected SqliteConnection GetConnection()
        {
            var connection = new SqliteConnection();

            if (_multipleSqlConnection)
                connection = BuildConnection();                                            
            else            
                connection = _sqlConnection ??= BuildConnection();            

            connection.Open();

            return connection;
        }
        protected async Task<IEnumerable<T>> ProcMultipleAsync<T>(string sql, object @params = null)
        {
            var connection = GetConnection();
            
            try
            {
                var result = await connection.QueryAsync<T>(sql, @params, commandType: System.Data.CommandType.StoredProcedure);
                return result;
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar verificar password. Erro: {ex.Message}");
            }
            finally
            {
                await connection.CloseAsync();
                await connection.DisposeAsync();
            }
        }
        protected async Task<T> ProcFirstAsync<T>(string sql, object @params = null)
        {
            var connection = GetConnection();
            try
            {
                var result = await connection.QueryFirstAsync<T>(sql, @params, commandType: CommandType.StoredProcedure);
                return result;
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar verificar password. Erro: {ex.Message}");
            }
            finally
            {
                await connection.CloseAsync();
                await connection.DisposeAsync();
            }

        }
        protected async Task<string> ProcJsonAsync(string command, Dictionary<string, object> parameters = null, CommandType type = CommandType.StoredProcedure)
        {
            using (var connection = GetConnection())
            {
                var cmd = new SqliteCommand(command, connection);
                cmd.CommandTimeout = 60;
                var json = string.Empty;
                cmd.CommandType = type;
                if (parameters != null)
                    foreach (var parameter in parameters)
                        cmd.Parameters.Add(new SqliteParameter(parameter.Key, parameter.Value));

                try
                {                    
                    var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                        if (reader[0] != null)
                            json = reader[0].ToString();
                }
                catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar verificar password. Erro: {ex.Message}");
            }
                finally
                {                    
                    await cmd.DisposeAsync();
                    await connection.CloseAsync();
                    await connection.DisposeAsync();
                }
                return json;
            }            
        }
        protected async Task<DynamicTableResult> ProcDynamicAsync(string command, object parameters = null)
        {
            var result = new DynamicTableResult();

            var connection = GetConnection();
            var cmd = GetCommand(command, parameters, GetConnection());

            try
            {                
                var reader = await cmd.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var row = new Dictionary<string, string>();

                        for (int i = 0; i < reader.FieldCount; i++) row.Add(reader.GetName(i).ToString(), reader.GetValue(i).ToString());

                        result.Data.Add(row);
                        result.RowCount++;
                    }
                }

            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar verificar password. Erro: {ex.Message}");
            }
            finally
            {
                connection.Dispose();
                cmd.Dispose();
            }

            return result;
        }
        
        private static SqliteCommand GetCommand(string command, object parameters, SqliteConnection connection)
        {
            var resul = new SqliteCommand(command.Trim(), connection);
            
            if (parameters != null)
            {
                foreach (var prop in parameters.GetType().GetProperties())
                {
                    resul.Parameters.AddWithValue(prop.Name, prop.GetValue(parameters));
                }
                resul.CommandType = System.Data.CommandType.StoredProcedure;
            }
            return resul;
        }
        
        public void Dispose()
        {            
            if(_sqlConnection is not null)
                _sqlConnection.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}