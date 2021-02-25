using System;
using System.Reflection;

namespace StateFlow
{
    public abstract class Workflow : StateManagement
    {
        public IServiceProvider Provider { get; }

        public Workflow(IServiceProvider provider)
        {
            Provider = provider;
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

    public interface IWorkflowService
    {
        
    }

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