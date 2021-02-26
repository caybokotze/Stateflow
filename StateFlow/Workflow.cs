using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace StateFlow
{
    public abstract class WorkflowEntity
    {
        public int Id { get; set; }
        public Guid ReferenceId { get; set; }
        public string Data { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateProcessed { get; set; }
    }
    
    public abstract class Workflow : StateManagement
    {
        public IServiceProvider Provider { get; }

        public Workflow(IServiceProvider provider)
        {
            Provider = provider;
        }
        
        public int Id { get; set; }
        private object Data { get; set; }

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

        public int WorkflowId { get; set; }
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