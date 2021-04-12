// Resharper disable CheckNamespace

using System;
using Stateflow.Entities;

namespace Stateflow
{
    public interface IWorkflowAction
    {
        public abstract void SetData(object obj);
        public abstract (object obj, Type type) GetData();
        void ExecuteAction();
    }
    
    public interface IWorkflowAction<out T>
    {
        T ExecuteAction();
    }
    
    public abstract class WorkflowAction : IWorkflowAction
    {
        protected WorkflowActionEntity WorkflowActionEntity { get; set; }
        public abstract void SetData(object obj);
        public abstract (object obj, Type type) GetData();

        public abstract void ExecuteAction();
    }
}