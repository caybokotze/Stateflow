
using System.Linq;
using Dapper;
using Stateflow.Entities;

// ReSharper disable once CheckNamespace
namespace Stateflow
{
    public static class StateManagementData
    {
        private static class Constants
        {
            public const string WorkflowTableName = "workflows";
            public const string WorkflowActionTableName = "workflow_actions";
        }
        
        public static int CreateWorkflowTable(
            IWorkflowService workflowService)
        {
            return workflowService.DbConnection
                .Query<int>(QueryBuilder
                        .CreateTableStatement<WorkflowEntity>(workflowService.DatabaseProvider,
                            new DbExecutionContext(Constants.WorkflowTableName, workflowService.Schema)))
                .FirstOrDefault();
        }

        public static int CreateOrUpdateWorkflow(IWorkflowService workflowService, WorkflowEntity workflowEntity)
        {
            return 0;
        }

        public static int CreateWorkflowActionTable()
        {
            return 0;
        }
    }
}