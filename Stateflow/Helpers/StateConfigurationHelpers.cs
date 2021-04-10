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
            var workflow = new WorkflowEntity()
            {
                StateName = eventConfigured.StateConfiguration.StateName
            };
            
            return new StateConfigured(eventConfigured.StateConfiguration);
        }
    }
}