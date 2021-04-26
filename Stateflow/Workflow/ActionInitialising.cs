using Stateflow.Entities;

// ReSharper disable CheckNamespace

namespace Stateflow
{
    public class ActionInitialising
    {
        public WorkflowActionEntity WorkflowActionEntity { get; }
        public WorkflowService WorkflowService { get; }

        public ActionInitialising(
            WorkflowActionEntity workflowActionEntity, 
            WorkflowService workflowService)
        {
            WorkflowActionEntity = workflowActionEntity;
            WorkflowService = workflowService;
        }
    }
}