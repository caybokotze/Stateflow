// ReSharper disable CheckNamespace
namespace Stateflow
{
    public interface IDbExecutionContext
    {
        public string Table { get; set; }
        public string Schema { get; set; }
    }

    public class DbExecutionContext : IDbExecutionContext
    {
        public DbExecutionContext(string table, string schema)
        {
            Table = table;
            Schema = schema;
        }
        
        public string Table { get; set; }
        public string Schema { get; set; }
    }
}