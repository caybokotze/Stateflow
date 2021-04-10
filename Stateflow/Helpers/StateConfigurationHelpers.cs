using System;
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

        public static void ThenChangeStateTo(this EventConfigured eventConfigured, string stateName)
        {
            eventConfigured
                .StateConfiguration
                .CurrentStateConfiguration
                .ChangeStateTo = stateName;
        }
        
        public static void ThenChangeStateTo(this EventConfigured eventConfigured, Enum stateName)
        {
            ThenChangeStateTo(eventConfigured, stateName.ToString());
        }
    }
}