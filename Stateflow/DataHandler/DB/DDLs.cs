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
                    .Query($@"CREATE TABLE IF NOT EXISTS `workflows`
                                (
                                    `id`            INT AUTO_INCREMENT PRIMARY KEY,
                                    `current_state` VARCHAR(255) NOT NULL,
                                    `uuid`          CHAR(36)     NOT NULL UNIQUE,
                                    `workflow_name` VARCHAR(255) UNIQUE,
                                    `date_created`  DATETIME,
                                    `date_modified` DATETIME,
                                    `is_active`     tinyint(1)
                                );");
            }

            public static void CreateWorkflowStatesTable(IWorkflowService workflowService)
            {
                workflowService.DbConnection
                    .Query($@"CREATE TABLE IF NOT EXISTS `workflow_states`
                                (
                                    `id`                   INT AUTO_INCREMENT PRIMARY KEY,
                                    `workflow_uuid`        CHAR(36)     NOT NULL,
                                    `registered_state`     VARCHAR(255) NOT NULL,
                                    `registered_action`    VARCHAR(255) NOT NULL,
                                    `registered_event`     VARCHAR(255) NOT NULL,
                                    `then_change_state_to` VARCHAR(255) NULL
                                );");
            }

            public static void CreateWorkflowActionsTable(IWorkflowService workflowService)
            {
                workflowService.DbConnection
                    .Query($@"CREATE TABLE IF NOT EXISTS `workflow_actions`
                                (
                                    `id`              BIGINT AUTO_INCREMENT PRIMARY KEY,
                                    `uuid`            CHAR(36)              NOT NULL UNIQUE,
                                    `workflowUuid`    CHAR(36)              NOT NULL,
                                    `retries`         INT                   NOT NULL,
                                    `action_body`     TEXT     DEFAULT NULL NULL,
                                    `action_name`     VARCHAR(255)          NOT NULL,
                                    `is_complete`     TINYINT(1),
                                    `date_expires`    DATETIME DEFAULT NULL NULL,
                                    `date_to_execute` DATETIME DEFAULT NULL NULL,
                                    `date_created`    DATETIME              NOT NULL,
                                    `date_modified`   DATETIME              NOT NULL,
                                    `date_processed`  DATETIME DEFAULT NULL NULL
                                );");
            }
        }
    }
}