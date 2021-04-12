using Stateflow.Entities;

namespace Stateflow
{
    public static class CreateWorkflowTableCommandBuilder
    {
        public static string CreateTableStatement(
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