// ReSharper disable once CheckNamespace
namespace Stateflow
{
    // ReSharper disable once InconsistentNaming
    public static class DDLBuilder
    {
        public static string CreateWorkflowTable()
        {
            return $@"CREATE TABLE IF NOT EXISTS `workflows`
                    (
                        `id`            INT AUTO_INCREMENT PRIMARY KEY,
                        `current_state` VARCHAR(255) NOT NULL,
                        `uuid`          CHAR(36)     NOT NULL UNIQUE,
                        `workflow_name` VARCHAR(255) UNIQUE,
                        `date_created`  DATETIME,
                        `date_modified` DATETIME,
                        `is_active`     tinyint(1)
                    );";
        }

        public static string CreateWorkflowStatesTable()
        {
            return $@"CREATE TABLE IF NOT EXISTS `workflow_states`
                    (
                        `id`                   INT AUTO_INCREMENT PRIMARY KEY,
                        `workflow_uuid`        CHAR(36)     NOT NULL,
                        `registered_state`     VARCHAR(255) NOT NULL,
                        `registered_action`    VARCHAR(255) NOT NULL,
                        `registered_event`     VARCHAR(255) NOT NULL,
                        `then_change_state_to` VARCHAR(255) NULL,
                        CONSTRAINT unique_constraint UNIQUE (`workflow_uuid`, `registered_state`, `registered_event`)
                    );";
        }

        public static string CreateWorkflowActionsTable()
        {
            return $@"CREATE TABLE IF NOT EXISTS `workflow_actions`
                    (
                        `id`              BIGINT AUTO_INCREMENT PRIMARY KEY,
                        `uuid`            CHAR(36)              NOT NULL UNIQUE,
                        `workflow_uuid`    CHAR(36)              NOT NULL,
                        `retries`         INT                   NOT NULL,
                        `action_body`     TEXT     DEFAULT NULL NULL,
                        `action_name`     VARCHAR(255)          NOT NULL,
                        `action_event`    VARCHAR(255)          NOT NULL,
                        `action_state`    VARCHAR(255)          NOT NULL,
                        `is_complete`     TINYINT(1),
                        `date_expires`    DATETIME              NOT NULL,
                        `date_to_execute` DATETIME DEFAULT NULL NULL,
                        `date_created`    DATETIME              NOT NULL,
                        `date_modified`   DATETIME              NOT NULL,
                        `date_processed`  DATETIME DEFAULT NULL NULL
                    );";
        }
    }
}