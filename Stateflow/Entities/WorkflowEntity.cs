using System;

namespace Stateflow.Entities
{
    public class WorkflowEntity
    {
        public int Id { get; set; }
        public string CurrentState { get; set; }
        public Guid Uuid { get; set; }
        public string WorkflowName { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsActive { get; set; }
    }
}