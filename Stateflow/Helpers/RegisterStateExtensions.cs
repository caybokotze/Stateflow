using System;

// ReSharper disable CheckNamespace
namespace Stateflow
{
    public static class RegisterStateExtensions
    {
        public static StateConfigured RegisterAction(
            this StateConfiguration stateConfiguration, 
            IWorkflowAction workflowAction)
        {
            stateConfiguration
                .CurrentState
                .RegisteredAction = workflowAction
                .GetType().Name;

            return new StateConfigured(stateConfiguration);
        }

        public static void ThenChangeStateTo(this EventConfigured eventConfigured, string stateName)
        {
            eventConfigured
                .StateConfiguration
                .CurrentState
                .ThenChangeStateTo = stateName;

            var currentState = eventConfigured.StateConfiguration.CurrentState;
            var workflowService = eventConfigured.StateConfiguration.WorkflowService;
            
            StateflowDbContext
                .Commands
                .CreateWorkflowState(workflowService, currentState);
        }
        
        public static void ThenChangeStateTo(this EventConfigured eventConfigured, Enum stateName)
        {
            ThenChangeStateTo(eventConfigured, stateName.ToString());
        }
        
        public static EventConfigured ExecuteActionOnEvent(
            this StateConfigured stateConfigured, 
            string eventName)
        {
            stateConfigured
                .StateConfiguration
                .CurrentState
                .RegisteredEvent = eventName;

            var workflowService = stateConfigured.StateConfiguration.WorkflowService;
            var currentState = stateConfigured.StateConfiguration.CurrentState;
            
            StateflowDbContext
                .Commands
                .CreateWorkflowState(workflowService, currentState);
            
            return new EventConfigured(stateConfigured.StateConfiguration);
        }
        
        public static EventConfigured ExecuteActionOnEvent(
            this StateConfigured stateConfigured,
            Enum eventName)
        {
            return ExecuteActionOnEvent(stateConfigured, eventName.ToString());
        }
    }
}