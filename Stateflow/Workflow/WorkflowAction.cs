// Resharper disable CheckNamespace

using System;

namespace Stateflow
{
    public interface IWorkflowAction
    {
        public abstract (object obj, Type type) SetData();
        void ExecuteAction();
    }
    
    public interface IWorkflowAction<out T>
    {
        T ExecuteAction();
    }
    
    public abstract class WorkflowAction : IWorkflowAction
    {
        public abstract (object obj, Type type) SetData();
        public abstract void ExecuteAction();
    }
    
    public abstract class WorkflowAction<T> : IWorkflowAction<T>
    {
        public abstract T ExecuteAction();
    }
}