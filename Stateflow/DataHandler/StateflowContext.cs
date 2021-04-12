using System;
using System.Linq;
using Dapper;
using SqExpress.SqlExport;
using Stateflow.Entities;

// ReSharper disable once CheckNamespace
namespace Stateflow
{
    public static class StateflowDbContext
    {
        private static class Constants
        {
            public const string WorkflowTableName = "workflows";
            public const string WorkflowActionsTableName = "workflow_actions";
            public const string WorkflowStatesTableName = "workflow_states";
        }

        public class Commands
        {
            public static int CreateWorkflowState(
                IWorkflowService workflowService, 
                WorkflowState workflowState)
            {
                return workflowService.DbConnection
                    .Query<int>(CommandBuilder.CreateOrUpdateWorkflowState(),
                        workflowState)
                    .FirstOrDefault();
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
        }

        public class Queries
        {
            public static WorkflowEntity FetchWorkflowByName(string name)
            {
                return new WorkflowEntity();
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

        // ReSharper disable once InconsistentNaming
        public class DDL
        {
            public static int CreateWorkflowTable(
                IWorkflowService workflowService)
            {
                return workflowService.DbConnection
                    .Query<int>(DDLBuilder.CreateWorkflowTable(workflowService.DatabaseProvider,
                            new DbExecutionContext(Constants.WorkflowTableName, workflowService.Schema)))
                    .FirstOrDefault();
            }

            public static int CreateWorkflowStatesTable()
            {
                return 0;
            }

            public static int CreateWorkflowActionsTable()
            {
                return 0;
            }
        }

        public static WorkflowEntity FetchWorkflowByUuid(Guid id)
        {
            return new WorkflowEntity
                {
                    
                };
        }
    }
}