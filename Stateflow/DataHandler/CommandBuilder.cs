
// ReSharper disable CheckNamespace

using Stateflow.Entities;

namespace Stateflow
{
    public static class CommandBuilder
    {
        public static string CreateOrUpdateWorkflow(WorkflowEntity workflowEntity)
        {
            return $@"
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
                                                        `date_modified` = utc_timestamp(3);";
        }

        public static string DeleteWorkflowByUuid()
        {
            return $@"DELETE FROM workflows WHERE uuid = @Uuid;";
        }

        public static string DeleteWorkflowActionsByWorkflowUuid()
        {
            return $@"DELETE FROM workflow_actions WHERE workflow_uuid = @Uuid;";
        }

        public static string DeleteWorkflowStatesByWorkflowUuid()
        {
            return $@"DELETE FROM workflow_states WHERE workflow_uuid = @Uuid";
        }

        public static string CreateOrUpdateWorkflowAction(
            WorkflowActionEntity workflowActionEntity)
        {
            return $@"INSERT INTO `workflow_actions` (
                                        `uuid`, 
                                        `workflow_uuid`, 
                                        `retries`, 
                                        `action_body`, 
                                        `action_name`, 
                                        `action_event`, 
                                        `action_state`,
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
                                            @{nameof(workflowActionEntity.ActionState)}, 
                                            @{nameof(workflowActionEntity.IsComplete)}, 
                                            @{nameof(workflowActionEntity.DateExpires)}, 
                                            @{nameof(workflowActionEntity.DateToExecute)}, 
                                            @{nameof(workflowActionEntity.DateCreated)}, 
                                            @{nameof(workflowActionEntity.DateModified)},
                                            @{nameof(workflowActionEntity.DateProcessed)})
                                    ON DUPLICATE KEY UPDATE `date_modified` = utc_timestamp(3),
                                                            `retries` = @{nameof(workflowActionEntity.Retries)},
                                                            `action_body` = @{nameof(workflowActionEntity.ActionBody)},
                                                            `date_modified` = utc_timestamp(3),
                                                            `date_processed` = @{nameof(workflowActionEntity.DateProcessed)};";
        }

        public static string CreateOrUpdateWorkflowState()
        {
            var query = $"INSERT INTO workflow_states (" +
                        "workflow_uuid, " +
                        "registered_state, " +
                        "registered_action, " +
                        "registered_event, " +
                        "then_change_state_to) " +
                        "VALUES( " +
                        "@WorkflowUuid, " +
                        "@RegisteredState, " +
                        "@RegisteredAction, " +
                        "@RegisteredEvent, " +
                        "@ThenChangeStateTo) " +
                        "ON DUPLICATE KEY UPDATE " +
                        "registered_action = @RegisteredAction, " +
                        "then_change_state_to = @ThenChangeStateTo;";

            return query;
        }
    }
}