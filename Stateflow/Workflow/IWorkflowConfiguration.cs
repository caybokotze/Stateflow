// ReSharper disable CheckNamespace
namespace Stateflow
{
    public interface IWorkflowConfiguration
    {
        IWorkflowService WorkflowService { get; set; }
    }
}