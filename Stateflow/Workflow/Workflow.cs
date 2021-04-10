using System;
using System.Collections.Generic;
using Stateflow.Entities;

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
        
        protected WorkflowEntity WorkflowEntity { get; set; }

        public abstract void RegisterStates();

        public new void RaiseEvent(string eventName)
        {
            base.RaiseEvent(eventName);
        }
        
        public void RaiseEvent(Enum eventName)
        {
            base.RaiseEvent(eventName.ToString());
        }

        public new void ForceStateOverride(string stateName)
        {
            base.ForceStateOverride(stateName);
        }

        public void ForceStateOverride(Enum stateName)
        {
            base.ForceStateOverride(stateName.ToString());
        }

        public int CompareTo(Workflow other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return WorkflowEntity.Id.CompareTo(other.WorkflowEntity.Id);
        }
    }
}