using System;
using System.Collections.Generic;

// ReSharper disable CheckNamespace
namespace Stateflow
{
    public class StateConfiguration
    {
        public class RegisteredState
        {
            public string CurrentState { get; set; }
            public string ActionName { get; set; }
            public string RaiseOnEvent { get; set; }
            public string ChangeStateTo { get; set;}
        }
        
        public RegisteredState CurrentStateConfiguration { get; set; }

        public void DisposeState()
        {
            CurrentStateConfiguration.ActionName = String.Empty;
            CurrentStateConfiguration.CurrentState = String.Empty;
            CurrentStateConfiguration.ChangeStateTo = String.Empty;
            CurrentStateConfiguration.RaiseOnEvent = String.Empty;
        }
        
        public List<RegisteredState> RegisteredStates { get; set; }
        
        public IWorkflowConfiguration Configuration { get; }

        public StateConfiguration(IWorkflowConfiguration configuration)
        {
            Configuration = configuration;
            Complete = false;
        }

        public bool Initialised { get; set; }
        public string WorkflowName { get; set; }
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