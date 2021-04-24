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
        public static ActionInitialising OnEvent(this ActionInitialising actionInitialising, string eventName)
        {
            actionInitialising.WorkflowActionEntity.ActionEvent = eventName;
            return actionInitialising;
        }

        public static void OnWorkflowEvent(this ActionInitialising actionInitialising, string stateName)
        {
            StateflowDbContext.Commands.CreateOrUpdateWorkflowAction(
                actionInitialising.WorkflowService, 
                actionInitialising.WorkflowActionEntity);
        }

        public static void OnWorkflowEvent(this ActionInitialising actionInitialising, Enum stateName)
        {
            OnWorkflowEvent(actionInitialising, stateName.ToString());
        }
        
        public static ActionInitialising OnEvent(this ActionInitialising actionInitialising, Enum eventName)
        {
            return OnEvent(actionInitialising, eventName.ToString());
        }
    }
}