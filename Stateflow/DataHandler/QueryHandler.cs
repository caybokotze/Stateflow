// ReSharper disable CheckNamespace

using System;
using SqExpress.SqlExport;
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
    
    public static class QueryHandler
    {
        public static string CreateTableStatement<T>(
            DatabaseProvider databaseProvider, 
            IDbExecutionContext executionContext)
        {
            var stateEntity = new SqExpressStateEntity(
                default,
                executionContext.Schema,
                executionContext.Table);
            
            var statement = string.Empty;
            var context = new ExecutingContext(new CreateMySqlTable(databaseProvider, statement, stateEntity));
            statement = context.Create();
            context = new ExecutingContext(new CreateMsSqlTable(databaseProvider, statement, stateEntity));
            statement = context.Create();
            context = new ExecutingContext(new CreatePostgreSqlTable(databaseProvider, statement, stateEntity));
            statement = context.Create();
            context = new ExecutingContext(new CreateSqLiteTable(databaseProvider, statement, stateEntity, executionContext));
            statement = context.Create();
            
            return statement;
        }
    }
    
    internal class CreatePostgreSqlTable : ContextExecutor
    {
        public CreatePostgreSqlTable(
            DatabaseProvider databaseProvider, 
            string sql,
            SqExpressStateEntity stateEntity) 
            : base(databaseProvider, sql, stateEntity) { }

        public override string Execute()
        {
            if (DatabaseProvider != DatabaseProvider.PostgreSql)
            {
                return Sql;
            }

            return PgSqlExporter
                .Default
                .ToSql(StateEntity
                    .Script
                    .DropAndCreate());
        }
    }

    internal class CreateMsSqlTable : ContextExecutor
    {
        public CreateMsSqlTable(
            DatabaseProvider databaseProvider, 
            string sql,
            SqExpressStateEntity stateEntity) 
            : base(databaseProvider, sql, stateEntity)
        {
            
        }
        
        public override string Execute()
        {
            if (DatabaseProvider != DatabaseProvider.MsSql)
            {
                return Sql;
            }
            
            var stateEntity = new SqExpressStateEntity();

            return TSqlExporter
                .Default
                .ToSql(stateEntity
                    .Script
                    .DropAndCreate());
        }
    }

    internal class CreateMySqlTable : ContextExecutor
    {
        public CreateMySqlTable(
            DatabaseProvider databaseProvider,
            string sql,
            SqExpressStateEntity stateEntity)
            : base(databaseProvider, sql, stateEntity)
        {
            
        }
        
        public override String Execute()
        {
            if (DatabaseProvider != DatabaseProvider.MySql)
            {
                return Sql;
            }

            return MySqlExporter
                .Default
                .ToSql(StateEntity
                    .Script
                    .DropAndCreate());
        }
    }
    
    internal class CreateSqLiteTable : ContextExecutor
    {
        public IDbExecutionContext Context { get; }

        public CreateSqLiteTable(
            DatabaseProvider databaseProvider,
            string sql,
            SqExpressStateEntity stateEntity,
            IDbExecutionContext context)
            : base(databaseProvider, sql, stateEntity)
        {
            Context = context;
        }
        
        public override String Execute()
        {
            if (DatabaseProvider != DatabaseProvider.MySql)
            {
                return Sql;
            }
            
            // since the lib don't support yo I will...
            return $"CREATE TABLE [IF NOT EXISTS] [${Context.Schema}].${Context.Schema} (" +
                   $"id INTEGER PRIMARY KEY, " +
                   $"message_id TEXT NOT NULL, " +
                   $"retries INTEGER NOT NULL, " +
                   $"body TEXT NOT NULL, " +
                   $"date_created TEXT NOT NULL " +
                   $"date_modified TEXT NOT NULL " +
                   $"date_processed TEXT DEFAULT NULL);";
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
        public string Sql { get; }
        public SqExpressStateEntity StateEntity { get; }

        protected ContextExecutor(
            DatabaseProvider databaseProvider, 
            string sql,
            SqExpressStateEntity stateEntity)
        {
            DatabaseProvider = databaseProvider;
            Sql = sql;
            StateEntity = stateEntity;
        }
        
        public abstract string Execute();
    }
}