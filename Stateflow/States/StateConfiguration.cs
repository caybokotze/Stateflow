using System;
using System.Collections.Generic;
using Stateflow.Entities;

// ReSharper disable CheckNamespace
namespace Stateflow
{
    public class StateConfiguration
    {
        public WorkflowState CurrentState { get; set; }
        public IWorkflowService WorkflowService { get; }

        public StateConfiguration(IWorkflowService workflowService)
        {
            WorkflowService = workflowService;
        }
    }
}