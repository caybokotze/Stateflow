// ReSharper disable CheckNamespace
namespace Stateflow
{
    public static class QueryBuilder
    {
        public static string FetchWorkflowByName()
        {
            return $@"SELECT * from workflows WHERE workflow_name = @Name;";
        }

        public static string FetchWorkflowActionByUuid()
        {
            return $@"SELECT * FROM workflow_actions WHERE uuid = @Uuid;";
        }

        public static string FetchActiveActionsByWorkflowUuid()
        {
            return $@"SELECT * FROM workflow_actions 
                    WHERE workflow_uuid = @WorkflowUuid 
                    AND is_complete = 0 
                    AND date_processed IS NULL;";
        }
    }
}