using System.Security.Principal;

// ReSharper disable CheckNamespace
namespace Stateflow
{
    public class StateConfiguration
    {
        public IWorkflowConfiguration Configuration { get; }

        public StateConfiguration(IWorkflowConfiguration configuration)
        {
            Configuration = configuration;
            Complete = false;
        }

        public bool Initialised { get; set; }
        public string StateName { get; set; }
        public bool Complete { get; set; }
    }
    
    public class WorkflowConfiguration : IWorkflowConfiguration
    {
        public WorkflowConfiguration(IWorkflowService workflowService)
        {
            WorkflowService = workflowService;
        }
        public IWorkflowService WorkflowService { get; set; }
    }
}