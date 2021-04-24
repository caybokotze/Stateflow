using System;
using Stateflow.Entities;
using Stateflow.Serializers;

// ReSharper disable CheckNamespace
namespace Stateflow
{
    public static class StateConfigurationHelpers
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
    }
}