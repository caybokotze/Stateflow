using SqExpress.SqlExport;
using Stateflow.Entities;
// ReSharper disable CheckNamespace
namespace Stateflow
{
    public static class CommandBuilder
    {
        public static string CreateOrUpdateWorkflow()
        {
            return string.Empty;
        }

        public static string CreateOrUpdateWorkflowAction()
        {
            return string.Empty;
        }

        public static string CreateOrUpdateWorkflowState(IDbExecutionContext executionContext)
        {
            var query = $"INSERT INTO {executionContext.Table} (" +
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