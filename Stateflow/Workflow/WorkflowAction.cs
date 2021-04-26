// Resharper disable CheckNamespace

using System;
using Stateflow.Entities;

namespace Stateflow
{
    public interface IWorkflowAction
    {
        public abstract object GetData();
        void ExecuteAction();
    }
    
    public interface IWorkflowAction<out T>
    {
        T ExecuteAction();
    }
    
    public abstract class WorkflowAction : IWorkflowAction
    {
        protected WorkflowActionEntity WorkflowActionEntity { get; set; }
        public abstract object GetData();
        public abstract void ExecuteAction();
    }
}