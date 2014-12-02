using ServiceStack.OrmLite;

namespace MvcStarterKit.Models
{
    public class DatabaseConfiguration
    {
        public string ConnectionString { get; set; }
        public IOrmLiteDialectProvider Dialect { get; set; }
    }
}