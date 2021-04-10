using System;
using SqExpress.SqlExport;
using Stateflow.Entities;


// ReSharper disable once CheckNamespace
namespace Stateflow
{
    public class WorkflowActivityQueries
    {
        internal class CreatePostgreSqlTable : ContextExecutor
    {
        public CreatePostgreSqlTable(
            DatabaseProvider databaseProvider, 
            string sql,
            SqActionEntity stateEntity) 
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
            SqActionEntity stateEntity) 
            : base(databaseProvider, sql, stateEntity)
        {
            
        }
        
        public override string Execute()
        {
            if (DatabaseProvider != DatabaseProvider.MsSql)
            {
                return Sql;
            }
            
            var stateEntity = new SqActionEntity();

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
            SqActionEntity stateEntity)
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
            SqActionEntity stateEntity,
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
        public SqActionEntity StateEntity { get; }

        protected ContextExecutor(
            DatabaseProvider databaseProvider, 
            string sql,
            SqActionEntity stateEntity)
        {
            DatabaseProvider = databaseProvider;
            Sql = sql;
            StateEntity = stateEntity;
        }
        
        public abstract string Execute();
    }
    }
}