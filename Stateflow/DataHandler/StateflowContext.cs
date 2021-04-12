using System;
using System.Linq;
using Dapper;
using Stateflow.Entities;

// ReSharper disable once CheckNamespace
namespace Stateflow
{
    public static class StateflowDbContext
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
        
        public static int CreateWorkflowActionTable(
            IWorkflowService workflowService)
        {
            return 0;
        }

        public static int CreateOrUpdateWorkflow(
            IWorkflowService workflowService, 
            WorkflowEntity workflowEntity)
        {
            return 0;
        }

        public static int CreateOrUpdateWorkflowAction(
            IWorkflowService workflowService, 
            WorkflowActionEntity workflowActionEntity)
        {
            return 0;
        }

        public static WorkflowEntity FetchWorkflowByUuid(Guid id)
        {
            return new WorkflowEntity
                {
                    
                };
        }

        public static WorkflowActionEntity FetchWorkflowEntityByUuid(Guid id)
        {
            return new WorkflowActionEntity();
        }

        public static WorkflowActions FetchWorkflowActionsByWorkflowUuid(Guid id)
        {
            return new WorkflowActions();
        }
        
        public static WorkflowActions FetchWorkflowActionsByWorkflowName(string name)
        {
            return new WorkflowActions();
        }
    }
}