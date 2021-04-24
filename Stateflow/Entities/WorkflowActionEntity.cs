using System;

namespace Stateflow.Entities
{
    public class WorkflowActionEntity
    {
        public ulong Id { get; set; }
        public Guid Uuid { get; set; }
        public Guid WorkflowUuid { get; set; }
        public int Retries { get; set; }
        public string ActionBody { get; set; }
        public string ActionName { get; set; }
        public string ActionEvent { get; set; }
        public string ActionState { get; set; }
        public bool IsComplete { get; set; }
        public DateTime? DateExpires { get; set; }
        public DateTime? DateToExecute { get; set; }
        public DateTime? DateProcessed { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}