using System;
using System.Collections.Generic;
using Stateflow.Entities;

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

        private WorkflowEntity WorkflowEntity { get; }
        
        public RegisteredState CurrentStateConfiguration { get; set; }

        public void DisposeState()
        {
            CurrentStateConfiguration.ActionName = String.Empty;
            CurrentStateConfiguration.CurrentState = String.Empty;
            CurrentStateConfiguration.ChangeStateTo = String.Empty;
            CurrentStateConfiguration.RaiseOnEvent = String.Empty;
        }
        
        public List<RegisteredState> RegisteredStates { get; set; }
        
        public IWorkflowConfiguration WorkflowConfiguration { get; }

        public StateConfiguration(IWorkflowConfiguration workflowConfiguration)
        {
            WorkflowConfiguration = workflowConfiguration;
        }
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