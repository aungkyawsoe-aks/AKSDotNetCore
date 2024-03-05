using System;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace AKSDotNetCore.Services
{
    public class DapperService
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder;

        public DapperService(SqlConnectionStringBuilder sqlConnectionStringBuilder)
        {
            _sqlConnectionStringBuilder = sqlConnectionStringBuilder;
        }

        public IEnumerable<T> Query<T>(string sql, object? param = null, CommandType commandType = CommandType.Text)
        {
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            return db.Query<T>(sql, commandType: commandType);
        }

        public T QueryFirstOrDefault<T>(string sql, object? param = null, CommandType commandType = CommandType.Text)
        {
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            return db.Query<T>(sql, commandType: commandType).FirstOrDefault()!;
        }

        public IEnumerable<dynamic> Query(string sql, object? param = null, CommandType commandType = CommandType.Text)
        {
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            return db.Query(sql, commandType: commandType);
        }

        public int Execute(string sql, object? param = null, CommandType commandType = CommandType.Text)
        {
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(sql, commandType: commandType);
            return result;
        }
    }
}
