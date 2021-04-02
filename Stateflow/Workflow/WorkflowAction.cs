// Resharper disable CheckNamespace
namespace Stateflow
{
    public interface IWorkflowAction
    {
        void ExecuteAction();
    }
    
    public interface IWorkflowAction<out T>
    {
        T ExecuteAction();
    }
    
    public abstract class WorkflowAction : IWorkflowAction
    {
        public abstract void ExecuteAction();
    }
    
    public abstract class WorkflowAction<T> : IWorkflowAction<T>
    {
        public abstract T ExecuteAction();
    }
}