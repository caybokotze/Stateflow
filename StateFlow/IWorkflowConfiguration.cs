namespace StateFlow
{
    public interface IWorkflowConfiguration
    {
        IWorkflowService WorkflowService { get; set; }
    }
}