using System.Collections.Generic;

namespace Stateflow.Entities
{
    public class WorkflowActions
    {
        public WorkflowEntity WorkflowEntity { get; set; }
        public IEnumerable<WorkflowActionEntity> WorkflowActionList { get; set; }
    }
}