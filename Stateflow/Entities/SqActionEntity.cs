using SqExpress;

namespace Stateflow.Entities
{
    public class SqActionEntity : TableBase
    {
        public SqActionEntity(): this(default, "", "")
        {
            
        }
        
        public SqActionEntity(Alias alias, string schema, string tableName) : base(schema, tableName, alias)
        {
            IsComplete = CreateBooleanColumn("is_complete");
            Id = CreateInt64Column("id", ColumnMeta.PrimaryKey().Identity());
            MessageId = CreateGuidColumn("message_id");
            Retries = CreateInt16Column("retries");
            Body = CreateStringColumn("body", size: 5000, isUnicode:true, isText: true);
            ActionName = CreateStringColumn("action_name", 500, isUnicode: true, isText: true);
            DateCreated = CreateDateTimeColumn("date_created");
            DateModified = CreateDateTimeColumn("date_modified");
            DateProcessed = CreateDateTimeColumn("date_processed");
        }

        public readonly Int64TableColumn Id;
        public readonly GuidTableColumn MessageId;
        public readonly Int16TableColumn Retries;
        public readonly StringTableColumn Body;
        public readonly StringTableColumn ActionName;
        public readonly BooleanTableColumn IsComplete;
        public readonly DateTimeTableColumn DateCreated;
        public readonly DateTimeTableColumn DateModified;
        public readonly DateTimeTableColumn DateProcessed;

    }
}