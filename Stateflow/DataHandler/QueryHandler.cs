// ReSharper disable CheckNamespace

using System;
using System.Runtime.InteropServices.ComTypes;
using SqExpress;
using SqExpress.SqlExport;

namespace Stateflow
{
    public enum Statement
    {
        Select,
        Create,
        Update,
        Delete
    }
    
    public static class QueryHandler
    {
        public static string CreateTableStatement<T>(DatabaseProvider databaseProvider)
        {
            var myType = typeof(T);
            var statement = "";
            
            var context = new ExecutingContext(new CreateMySqlTable(databaseProvider));
            statement = context.Create();
            context = new ExecutingContext(new CreateMsSqlTable(databaseProvider));
            statement = context.Create();
            context = new ExecutingContext(new CreateMsSqlTable(databaseProvider));
            statement = context.Create();
            context = new ExecutingContext(new CreateMsSqlTable(databaseProvider));
            statement = context.Create();
            
            return statement;
        }
    }
    
    internal class CreatePostgreSqlTable : ContextExecutor
    {
        public CreatePostgreSqlTable(DatabaseProvider databaseProvider) : base(databaseProvider)
        {
        }

        public override string Execute()
        {
            if (DatabaseProvider == DatabaseProvider.PostgreSql)
            {
                
            }

            return "";
        }
    }

    internal class CreateMsSqlTable : ContextExecutor
    {
        public CreateMsSqlTable(DatabaseProvider databaseProvider) : base(databaseProvider)
        {
            
        }
        
        public override string Execute()
        {
            if (DatabaseProvider == DatabaseProvider.MsSql)
            {
                
            }

            return "";
        }
    }

    internal class CreateMySqlTable : ContextExecutor
    {
        public CreateMySqlTable(DatabaseProvider databaseProvider) : base(databaseProvider)
        {
            
        }
        
        public override String Execute()
        {
            if (DatabaseProvider == DatabaseProvider.MySql)
            {
                // var query = SqQueryBuilder.Select()
                // MySqlExporter.Default.ToSql()
            }

            return "";
        }
    }

    internal class ExecutingContext
    {
        public ExecutingContext(ContextExecutor contextExecutor)
        {
            ContextExecutor = contextExecutor;
        }

        private ContextExecutor ContextExecutor { get; }
        
        public string Create()
        {
            return ContextExecutor.Execute();
        }
    }

    public abstract class ContextExecutor
    {
        public DatabaseProvider DatabaseProvider { get; }

        protected ContextExecutor(DatabaseProvider databaseProvider)
        {
            DatabaseProvider = databaseProvider;
        }
        
        public abstract string Execute();
    }
}