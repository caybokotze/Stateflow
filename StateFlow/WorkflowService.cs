using System;

namespace StateFlow
{
    public class WorkflowService<T> : IWorkflowService where T : Workflow
    {
        public IServiceProvider Provider { get; }

        public WorkflowService(IServiceProvider provider)
        {
            Provider = provider;
        }
        
        public void RaiseEvent(Enum eventName)
        {
            Activator.CreateInstance<T>()
                .RaiseEvent(eventName);
        }
    }
}