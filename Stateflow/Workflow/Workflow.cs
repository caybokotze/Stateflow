using System;

// ReSharper disable CheckNamespace
namespace Stateflow
{
    public abstract class Workflow : StateManager
    {
        public Workflow(
            IWorkflowService workflowService) :
            base(workflowService)
        {
        }
        
        private object Data { get; set; }

        public ulong WorkflowId { get; set; }
        
        public void SetData<T>(object obj) where T : WorkflowEntity
        {
            Data = obj;
        }

        public abstract string Register();

        public virtual void RaiseEvent(Enum eventName)
        {
            return;
        }
        
        public virtual void RaiseEvent(string eventName)
        {
            return;
        }
    }
}