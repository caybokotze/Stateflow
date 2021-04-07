using System;
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
    
    public class SqExpressStateEntity : TableBase, IWorkflowStateEntity
    {
        
        public SqExpressStateEntity(): this(default, "", "")
        {
            
        }
        public SqExpressStateEntity(Alias alias, string schema, string tableName) : base(schema, tableName, alias)
        {
            // this.Id = this.CreateInt64Column("id")
        }

        public ulong Id { get; set; }
        public Guid MessageId { get; set; }
        public int Retries { get; set; }
        public string Body { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateProcessed { get; set; }
    }
}