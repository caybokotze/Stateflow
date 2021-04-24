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

        public static StateComplete ThenChangeStateTo(this EventConfigured eventConfigured, string stateName)
        {
            eventConfigured
                .StateConfiguration
                .CurrentState
                .ThenChangeStateTo = stateName;
            
            return new StateComplete(eventConfigured.StateConfiguration);
        }
        
        public static StateComplete ThenChangeStateTo(this EventConfigured eventConfigured, Enum stateName)
        {
            return ThenChangeStateTo(eventConfigured, stateName.ToString());
        }

        public static void SaveState(this StateComplete stateComplete)
        {
            var currentState = stateComplete
                .StateConfiguration
                .CurrentState;

            var workflowAction = new WorkflowActionEntity
            {
                Uuid = Guid.NewGuid(),
                WorkflowUuid = currentState.WorkflowUuid,
                Retries = 0,
                ActionName = currentState.RegisteredAction,
                ActionEvent = currentState.RegisteredEvent,
                DateProcessed = null
            };

            try
            {
                StateflowDbContext
                    .Commands
                    .CreateWorkflowAction(stateComplete.StateConfiguration.WorkflowService,
                        workflowAction);
            }
            catch (Exception e)
            {
                // ignore
                Console.WriteLine(e.Message);
            }

            StateflowDbContext
                .Commands
                .CreateWorkflowState(stateComplete
                    .StateConfiguration
                    .WorkflowService, currentState);
        }

        public static void SaveState(this EventConfigured eventConfigured)
        {
            var stateComplete = new StateComplete(eventConfigured.StateConfiguration);
            stateComplete.SaveState();
        }
    }
}