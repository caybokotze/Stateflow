using System;

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
}