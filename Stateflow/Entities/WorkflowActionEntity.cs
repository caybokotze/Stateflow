using System;

namespace Stateflow.Entities
{
    public class WorkflowActionEntity
    {
        public ulong Id { get; set; }
        public Guid Uuid { get; set; }
        public int Retries { get; set; }
        public string ActionBody { get; set; }
        public string ActionName { get; set; }
        public bool IsComplete { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateProcessed { get; set; }
    }
}