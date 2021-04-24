// ReSharper disable CheckNamespace

using Dapper;

namespace Stateflow
{
    public static partial class StateflowDbContext
    {
        // ReSharper disable once InconsistentNaming
        public static class DDL
        {
            public static void CreateWorkflowTable(
                IWorkflowService workflowService)
            {
                workflowService.DbConnection
                    .Query(DDLBuilder.CreateWorkflowTable());
            }

            public static void CreateWorkflowStatesTable(IWorkflowService workflowService)
            {
                workflowService.DbConnection
                    .Query(DDLBuilder.CreateWorkflowStatesTable());
            }

            public static void CreateWorkflowActionsTable(IWorkflowService workflowService)
            {
                workflowService.DbConnection
                    .Query(DDLBuilder.CreateWorkflowActionsTable());
            }
        }
    }
}