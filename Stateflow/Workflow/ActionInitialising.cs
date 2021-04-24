using Stateflow.Entities;
using Enum = System.Enum;

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
    
    public static class WorkflowServiceExtensions {
        public static ActionInitialising OnWorkflowEvent(this ActionInitialising actionInitialising, string eventName)
        {
            actionInitialising.WorkflowActionEntity.ActionEvent = eventName;
            return actionInitialising;
        }

        public static void OnWorkflowState(this ActionInitialising actionInitialising, string stateName)
        {
            StateflowDbContext.Commands.CreateOrUpdateWorkflowAction(
                actionInitialising.WorkflowService, 
                actionInitialising.WorkflowActionEntity);
        }

        public static void OnWorkflowState(this ActionInitialising actionInitialising, Enum stateName)
        {
            OnWorkflowState(actionInitialising, stateName.ToString());
        }
        
        public static ActionInitialising OnWorkflowEvent(this ActionInitialising actionInitialising, Enum eventName)
        {
            return OnWorkflowEvent(actionInitialising, eventName.ToString());
        }
    }
}