using System;
using SqExpress;

namespace Stateflow.Entities
{
    public class WorkflowState
    {
        public int Id { get; set; }
        public Guid WorkflowUuid { get; set; }
        public string RegisteredState { get; set; }
        public string RegisteredAction { get; set; }
        public string RegisteredEvent { get; set; }
        public string ThenChangeStateTo { get; set; }
    }

    public class SqWorkflowState : TableBase
    {
        public SqWorkflowState(string schema, string tableName) 
            : this(default, schema, tableName)
        {
        }

        public SqWorkflowState(Alias alias, string schema, string tableName) 
            : base(schema, tableName, alias)
        {
            Id = CreateInt32Column("id", ColumnMeta.PrimaryKey().Identity());
            WorkflowUuid = CreateGuidColumn("workflow_uuid");
            RegisteredState = CreateStringColumn("registered_state", size: 255, isUnicode: true, isText: true);
            RegisteredAction = CreateStringColumn("registered_action", size: 255, isText: true, isUnicode: true);
            RegisteredEvent = CreateStringColumn("registered_event", 255, isText: true, isUnicode: true);
            ThenChangeStateTo = CreateStringColumn("then_change_state_to", 255, isText: true, isUnicode: true);
        }

        public readonly Int32TableColumn Id;
        public readonly GuidTableColumn WorkflowUuid;
        public readonly StringTableColumn RegisteredState;
        public readonly StringTableColumn RegisteredAction;
        public readonly StringTableColumn RegisteredEvent;
        public readonly StringTableColumn ThenChangeStateTo;
    }
}