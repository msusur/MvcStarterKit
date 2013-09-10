using System;
using System.Configuration;
using System.Data;
using MvcStarterKit.Exceptions;
using ServiceStack.OrmLite;

namespace MvcStarterKit.Services
{
    /// <summary>
    /// Helper class for opening a database connection.
    /// </summary>
    public static class DbHelper
    {
        private static OrmLiteConnectionFactory _dbFactory;

        public static IDbConnection CreateConnection()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            if (connectionString == null)
            {
                throw new ConnectionStringNotFoundException();
            }
            if (_dbFactory == null)
            {
                _dbFactory = new OrmLiteConnectionFactory(connectionString.ConnectionString, SqlServerDialect.Provider);
            }
            return _dbFactory.OpenDbConnection();
        }

        public static void Execute(Action<IDbConnection> connection)
        {
            using (var db = CreateConnection())
            {
                connection(db);
            }
        }
    }
}