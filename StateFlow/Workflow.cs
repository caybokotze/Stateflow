using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace StateFlow
{
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
}