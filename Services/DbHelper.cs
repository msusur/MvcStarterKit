using System;
using System.Configuration;
using System.Data;
using MvcStarterKit.Exceptions;
using MvcStarterKit.Models;
using ServiceStack.OrmLite;

namespace MvcStarterKit.Services
{
    /// <summary>
    /// Helper class for opening a database connection.
    /// </summary>
    public static class DbHelper
    {
        private static OrmLiteConnectionFactory _dbFactory;

        private static DatabaseConfiguration _configuration;

        public static IDbConnection CreateConnection()
        {
            if (_dbFactory == null)
            {
                if (_configuration == null)
                {
                    _configuration = LoadConfiguration();
                }
                _dbFactory = new OrmLiteConnectionFactory(_configuration.ConnectionString, _configuration.Dialect);
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

        private static DatabaseConfiguration LoadConfiguration()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            if (connectionString == null)
            {
                throw new ConnectionStringNotFoundException();
            }

            IOrmLiteDialectProvider provider = DialectProviderFactory.GetDialectProvider(connectionString.ProviderName);

            return new DatabaseConfiguration
            {
                ConnectionString = connectionString.ConnectionString,
                Dialect = provider
            };
        }

        internal static void Configure(DatabaseConfiguration configuration)
        {
            _configuration = configuration;
        }
    }

    internal static class DialectProviderFactory
    {
        public static IOrmLiteDialectProvider GetDialectProvider(string providerName)
        {
            switch (providerName.ToLowerInvariant())
            {
                case "mysql.data.mysqlclient":
                    return MySqlDialect.Provider;
                default:
                case "system.data.sqlclient":
                    return SqlServerDialect.Provider;

            }
        }
    }
}