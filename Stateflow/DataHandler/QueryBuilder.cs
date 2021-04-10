// ReSharper disable CheckNamespace

using System;
using Stateflow.Entities;

namespace Stateflow
{
    public enum Statement
    {
        Select,
        Create,
        Update,
        Delete
    }

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
    
    public static class QueryBuilder
    {
        public static string CreateTableStatement<T>(
            DatabaseProvider databaseProvider, 
            IDbExecutionContext executionContext)
        {
            var stateEntity = new SqActionEntity(
                default,
                executionContext.Schema,
                executionContext.Table);

            var statement = string.Empty;
            var context = new WorkflowActivityQueries
                .ExecutingContext(new WorkflowActivityQueries
                    .CreateMySqlTable(databaseProvider, statement, stateEntity));
            statement = context.Create();
            
            context = new WorkflowActivityQueries
                .ExecutingContext(new WorkflowActivityQueries
                    .CreateMsSqlTable(databaseProvider, statement, stateEntity));
            statement = context.Create();
            
            context = new WorkflowActivityQueries
                .ExecutingContext(new WorkflowActivityQueries
                    .CreatePostgreSqlTable(databaseProvider, statement, stateEntity));
            statement = context.Create();
            
            context = new WorkflowActivityQueries
                .ExecutingContext(new WorkflowActivityQueries
                    .CreateSqLiteTable(databaseProvider, statement, stateEntity, executionContext));
            statement = context.Create();

            return statement;
        }
    }
}