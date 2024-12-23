using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ChatDemo.Services
{
    public interface IDatabaseService
        {
            IDbConnection CreateConnection();
        }
    public class DapperDatabaseService: IDatabaseService
    {
        private readonly string _connectionString;
        

        public DapperDatabaseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
