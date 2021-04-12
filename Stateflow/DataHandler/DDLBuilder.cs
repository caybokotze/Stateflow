﻿// ReSharper disable once CheckNamespace
namespace Stateflow
{
    // ReSharper disable once InconsistentNaming
    public static class DDLBuilder
    {
        public static string CreateWorkflowTable(
            DatabaseProvider databaseProvider,
            IDbExecutionContext executionContext)
        {
            return CreateWorkflowTableCommandBuilder
                .CreateTableStatement(databaseProvider, executionContext);
        }
    }
}