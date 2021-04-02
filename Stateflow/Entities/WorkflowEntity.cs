namespace Stateflow.Entities
{
    public class WorkflowEntity
    {
        public WorkflowEntity()
        {
            Complete = false;
        }
        
        public bool Initialised { get; set; }
        public string StateName { get; set; }
        public bool Complete { get; set; }
        public ulong Id { get; set; }
        public object Data { get; set; }
        public string WorkflowName { get; set; }
    }
}