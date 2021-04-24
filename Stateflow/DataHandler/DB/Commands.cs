using System;
using System.Linq;
using Dapper;
using Stateflow.Entities;

// ReSharper disable once CheckNamespace
namespace Stateflow
{
    public static partial class StateflowDbContext
    {
        private static class Constants
        {
            public const string WorkflowTableName = "workflows";
            public const string WorkflowActionsTableName = "workflow_actions";
            public const string WorkflowStatesTableName = "workflow_states";
        }

        public static class Commands
        {
            public static int CreateWorkflowState(
                IWorkflowService workflowService, 
                WorkflowState workflowState)
            {
                return workflowService.DbConnection
                    .Query<int>(CommandBuilder
                            .CreateOrUpdateWorkflowState(new DbExecutionContext
                            (Constants.WorkflowStatesTableName,
                                workflowService.Schema)),
                        workflowState)
                    .FirstOrDefault();
            }
            
            public static int CreateOrUpdateWorkflow(
                IWorkflowService workflowService, 
                WorkflowEntity workflowEntity)
            {
                return workflowService.DbConnection
                    .Query<int>($@"
                    INSERT INTO `workflows` (
                        `current_state`,
                        `uuid`,
                        `workflow_name`,
                        `date_created`,
                        `date_modified`,
                        `is_active`)
                    VALUES (@{nameof(workflowEntity.CurrentState)},
                            @{nameof(workflowEntity.Uuid)},
                            @{nameof(workflowEntity.WorkflowName)},
                            utc_timestamp(3),
                            utc_timestamp(3),
                            @{nameof(workflowEntity.IsActive)})
                    ON DUPLICATE KEY UPDATE `current_state` = @{nameof(workflowEntity.CurrentState)},
                                            `date_modified` = utc_timestamp(3);", workflowEntity)
                    .FirstOrDefault();
            }
            
            public static int CreateOrUpdateWorkflowAction(
                IWorkflowService workflowService, 
                WorkflowActionEntity workflowActionEntity)
            {
                return workflowService.DbConnection
                    .Query<int>($@"INSERT INTO `workflow_actions` (
                                        `uuid`, 
                                        `workflow_uuid`, 
                                        `retries`, 
                                        `action_body`, 
                                        `action_name`, 
                                        `action_event`, 
                                        `is_complete`, 
                                        `date_expires`,
                                        `date_to_execute`, 
                                        `date_created`, 
                                        `date_modified`, 
                                        `date_processed`) 
                                    VALUES (@{nameof(workflowActionEntity.Uuid)},
                                            @{nameof(workflowActionEntity.WorkflowUuid)},
                                            @{nameof(workflowActionEntity.Retries)},
                                            @{nameof(workflowActionEntity.ActionBody)}, 
                                            @{nameof(workflowActionEntity.ActionName)},
                                            @{nameof(workflowActionEntity.ActionEvent)}, 
                                            @{nameof(workflowActionEntity.IsComplete)}, 
                                            @{nameof(workflowActionEntity.DateExpires)}, 
                                            @{nameof(workflowActionEntity.DateToExecute)}, 
                                            @{nameof(workflowActionEntity.DateCreated)}, 
                                            @{nameof(workflowActionEntity.DateModified)},
                                            @{nameof(workflowActionEntity.DateProcessed)})
                                    ON DUPLICATE KEY UPDATE `date_modified` = utc_timestamp(3),
                                                            `retries` = @{nameof(workflowActionEntity.Retries)},
                                                            `action_body` = @{nameof(workflowActionEntity.ActionBody)},
                                                            `date_to_execute` = @{nameof(workflowActionEntity.DateToExecute)},
                                                            `date_expires` = @{nameof(workflowActionEntity.DateExpires)},
                                                            `date_processed` = @{nameof(workflowActionEntity.DateProcessed)};", 
                        workflowActionEntity)
                    .FirstOrDefault();
            }

            public static int CreateWorkflowAction(
                IWorkflowService workflowService, 
                WorkflowActionEntity workflowActionEntity)
            {
                return workflowService.DbConnection
                    .Query<int>($@"INSERT INTO `workflow_actions` (
                                        `uuid`, 
                                        `workflow_uuid`, 
                                        `retries`, 
                                        `action_body`, 
                                        `action_name`, 
                                        `action_event`, 
                                        `is_complete`, 
                                        `date_expires`,
                                        `date_to_execute`, 
                                        `date_created`, 
                                        `date_modified`, 
                                        `date_processed`) 
                                    VALUES (@{nameof(workflowActionEntity.Uuid)},
                                            @{nameof(workflowActionEntity.WorkflowUuid)},
                                            @{nameof(workflowActionEntity.Retries)},
                                            @{nameof(workflowActionEntity.ActionBody)}, 
                                            @{nameof(workflowActionEntity.ActionName)},
                                            @{nameof(workflowActionEntity.ActionEvent)}, 
                                            @{nameof(workflowActionEntity.IsComplete)}, 
                                            @{nameof(workflowActionEntity.DateExpires)}, 
                                            @{nameof(workflowActionEntity.DateToExecute)}, 
                                            utc_timestamp(3), 
                                            utc_timestamp(3),
                                            @{nameof(workflowActionEntity.DateProcessed)});", 
                        workflowActionEntity)
                    .FirstOrDefault();
            }
            
            public static void DeleteWorkflowByUuid(
                IWorkflowService workflowService, 
                Guid uuid)
            {
                workflowService
                    .DbConnection
                    .Query("DELETE FROM workflows WHERE uuid = @Uuid",
                    new { Uuid = uuid});
            }

            public static void DeleteActionsByWorkflowUuid(
                IWorkflowService workflowService, 
                Guid uuid)
            {
                
            }
            
            public static void DeleteWorkflowActionsByWorkflowUuid(
                IWorkflowService workflowService, 
                Guid uuid)
            {
                workflowService
                    .DbConnection
                    .Query("DELETE FROM workflow_actions WHERE workflow_uuid = @Uuid",
                        new { Uuid = uuid });
            }

            public static void DeleteWorkflowStatesByWorkflowUuid(
                IWorkflowService workflowService,
                Guid uuid)
            {
                workflowService
                    .DbConnection
                    .Query("DELETE FROM workflow_states WHERE workflow_uuid = @Uuid",
                        new { Uuid = uuid });
            }
        }
    }
}