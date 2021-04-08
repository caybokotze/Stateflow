using System;
using Microsoft.Extensions.DependencyInjection;
using SqExpress;
using SqExpress.Syntax.Names;

namespace Stateflow.Entities
{
    public interface IWorkflowStateEntity
    {
        public ulong Id { get; set; }
        public Guid MessageId { get; set; }
        public int Retries { get; set; }
        public string Body { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateProcessed { get; set; }
    }
    
    public class SqExpressStateEntity : TableBase
    {
        public SqExpressStateEntity(): this(default, "", "")
        {
            
        }
        public SqExpressStateEntity(Alias alias, string schema, string tableName) : base(schema, tableName, alias)
        {
            Id = CreateInt64Column("id", ColumnMeta.PrimaryKey().Identity());
            MessageId = CreateGuidColumn("message_id");
            Retries = CreateInt16Column("retries");
            Body = CreateStringColumn("body", size: 5000, isUnicode:true, isText: true);
            DateCreated = CreateDateTimeColumn("date_created");
            DateModified = CreateDateTimeColumn("date_modified");
            DateProcessed = CreateDateTimeColumn("date_processed");
        }

        public readonly Int64TableColumn Id;
        public readonly GuidTableColumn MessageId;
        public readonly Int16TableColumn Retries;
        public readonly StringTableColumn Body;
        public readonly DateTimeTableColumn DateCreated;
        public readonly DateTimeTableColumn DateModified;
        public readonly DateTimeTableColumn DateProcessed;

    }
}