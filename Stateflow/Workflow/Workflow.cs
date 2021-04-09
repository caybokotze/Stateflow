using System;
using System.Collections.Generic;

// ReSharper disable CheckNamespace
namespace Stateflow
{
    public abstract class Workflow : StateManager, IComparable<Workflow>
    {
        
        
        public Workflow(
            IWorkflowService workflowService) :
            base(workflowService)
        {
        }
        
        private object Data { get; set; }
        private Type Type { get; set; }
        public ulong WorkflowId { get; set; }
        
        
        public void SetData<T>(object obj) where T : WorkflowEntity
        {
            Type = typeof(T);
            Data = obj;
        }

        public abstract void RegisterStates();

        public new void RaiseEvent(string eventName)
        {
            base.RaiseEvent(eventName);
        }

        public new void ForceStateOverride(string stateName)
        {
            base.ForceStateOverride(stateName);
        }

        public void ForceStateOverride(Enum stateName)
        {
            base.ForceStateOverride(stateName.ToString());
        }
        
        public void RaiseEvent(Enum eventName)
        {
            base.RaiseEvent(eventName.ToString());
        }

        public int CompareTo(Workflow other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return WorkflowId.CompareTo(other.WorkflowId);
        }
    }
}