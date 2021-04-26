using System;
using System.Collections.Generic;
using Stateflow.Entities;

// ReSharper disable CheckNamespace
namespace Stateflow
{
    public abstract class Workflow : StateManager, IComparable<Workflow>
    {
        public IWorkflowService WorkflowService { get; }

        public Workflow(
            IWorkflowService workflowService) :
            base(workflowService)
        {
            WorkflowService = workflowService;
        }
        
        protected WorkflowEntity WorkflowEntity { get; set; }

        public abstract void DefineWorkflowRules();

        public int CompareTo(Workflow other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return WorkflowEntity.Id.CompareTo(other.WorkflowEntity.Id);
        }
    }
}