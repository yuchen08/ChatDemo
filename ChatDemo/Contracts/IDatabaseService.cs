using System.Data;

namespace ChatDemo.Contracts
{
    /// <summary>
    /// 資料庫服務介面契約
    /// 定義資料庫連接的基本操作
    /// </summary>
    public interface IDatabaseService
    {
        /// <summary>
        /// 建立資料庫連接
        /// </summary>
        /// <returns>資料庫連接物件</returns>
        IDbConnection CreateConnection();
    }
} 