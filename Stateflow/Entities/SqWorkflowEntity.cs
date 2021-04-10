using System;
using SqExpress;

namespace Stateflow.Entities
{
    public class SqWorkflowEntity : TableBase
    {
        public SqWorkflowEntity(string schema, string tableName) : this(default, schema, tableName)
        {
        }

        public SqWorkflowEntity(Alias alias, string schema, string tableName) : base(schema, tableName, alias)
        {
            Id = CreateInt32Column("id", ColumnMeta.PrimaryKey().Identity());
            Uuid = CreateGuidColumn("uuid");
            WorkflowName = CreateStringColumn("workflow_name", size: 5000, isUnicode: true, isText: true);
            CurrentState = CreateStringColumn("current_state", size: 200, isText: true, isUnicode: true);
            IsActive = CreateBooleanColumn("is_active");
            DateCreated = CreateDateTimeColumn("date_created");
        }

        public readonly Int32TableColumn Id;
        public readonly GuidTableColumn Uuid;
        public readonly StringTableColumn WorkflowName;
        public readonly StringTableColumn CurrentState;
        public readonly BooleanTableColumn IsActive;
        public readonly DateTimeTableColumn DateCreated;
    }
}