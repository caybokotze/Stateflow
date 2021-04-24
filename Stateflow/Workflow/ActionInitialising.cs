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
        public static void OnEvent(this ActionInitialising actionInitialising, string eventName)
        {
            actionInitialising.WorkflowActionEntity.ActionEvent = eventName;
            
            StateflowDbContext.Commands.CreateOrUpdateWorkflowAction(
                actionInitialising.WorkflowService, 
                actionInitialising.WorkflowActionEntity);
        }

        public static void OnWorkflowEvent(this ActionInitialising actionInitialising, string stateName)
        {
            
        }
        
        public static void OnEvent(this ActionInitialising actionInitialising, Enum eventName)
        {
            OnEvent(actionInitialising, eventName.ToString());
        }
    }
}