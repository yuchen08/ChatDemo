using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ChatDemo.Contracts;

namespace ChatDemo.Implementation
{
    /// <summary>
    /// Dapper 資料庫服務實作
    /// 使用 Dapper 微 ORM 框架進行資料庫操作
    /// </summary>
    public class DapperDatabaseService : IDatabaseService
    {
        private readonly string _connectionString;

        /// <summary>
        /// 建構函數
        /// </summary>
        /// <param name="configuration">配置服務</param>
        public DapperDatabaseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? throw new ArgumentNullException(nameof(configuration), "資料庫連接字串未配置");
        }

        /// <summary>
        /// 建立資料庫連接
        /// </summary>
        /// <returns>SQL Server 連接物件</returns>
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
} 