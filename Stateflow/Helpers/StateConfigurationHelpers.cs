using System;
using System.Linq;
using Stateflow.Entities;

// ReSharper disable CheckNamespace
namespace Stateflow
{
    public static class StateConfigurationHelpers
    {
        public static StateConfigured RegisterEvent<T>(this StateConfiguration stateConfiguration) where T : IRegisteredEvent
        {
            return new StateConfigured(stateConfiguration);
        }

        public static StateConfigured RegisterAction(
            this StateConfiguration stateConfiguration, 
            IWorkflowAction workflowAction)
        {
            stateConfiguration
                .CurrentStateConfiguration
                .ActionName = workflowAction.GetType().ToString();
            

            return new StateConfigured(stateConfiguration);
        }

        public static StateComplete ThenChangeStateTo(this EventConfigured eventConfigured, string stateName)
        {
            eventConfigured
                .StateConfiguration
                .CurrentStateConfiguration
                .ChangeStateTo = stateName;
            
            return new StateComplete(eventConfigured.StateConfiguration);
        }
        
        public static StateComplete ThenChangeStateTo(this EventConfigured eventConfigured, Enum stateName)
        {
            return ThenChangeStateTo(eventConfigured, stateName.ToString());
        }

        public static void SaveState(this StateComplete stateComplete)
        {
            var currentState = stateComplete.StateConfiguration.CurrentStateConfiguration;
            
            stateComplete
                .StateConfiguration
                .RegisteredStates
                .Add(new StateConfiguration.RegisteredState()
            {
                ActionName = currentState.ActionName,
                CurrentState = currentState.CurrentState,
                ChangeStateTo = currentState.ChangeStateTo,
                RaiseOnEvent = currentState.RaiseOnEvent
            });
        }
    }
}