using System;

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
                .Action = workflowAction
                .GetType()
                .GetProperties()
                .ToString();
            
            return new StateConfigured(stateConfiguration);
        }

        public static StateConfigured ThenChangeStateTo(this EventConfigured eventConfigured, string stateName)
        {
            eventConfigured
                .StateConfiguration
                .CurrentStateConfiguration
                .ChangeStateTo = stateName;
            
            return new StateConfigured(eventConfigured.StateConfiguration);
        }
        
        public static StateConfigured ThenChangeStateTo(this EventConfigured eventConfigured, Enum stateName)
        {
            return new StateConfigured(eventConfigured.StateConfiguration);
        }
    }
}