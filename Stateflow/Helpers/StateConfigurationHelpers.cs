using System;

// ReSharper disable CheckNamespace
namespace Stateflow
{
    public static class StateConfigurationHelpers
    {
        public static StateConfigured RegisterEvent<T>(this StateConfiguration configuration) where T : IRegisteredEvent
        {
            
            return new StateConfigured(configuration.Configuration);
        }

        public static StateConfigured RegisterAction(this StateConfiguration configuration, Action action)
        {
            return new StateConfigured(configuration.Configuration);
        }
        
        public static StateConfigured RegisterAction(this StateConfiguration configuration, IWorkflowAction workflowAction)
        {
            return new StateConfigured(configuration.Configuration);
        }

        public static StateConfigured ThenChangeStateTo(this EventConfigured configured, string stateName)
        {
            return new StateConfigured(configured.Configuration);
        }
        
        public static StateConfigured ThenChangeStateTo(this EventConfigured configured, Enum stateName)
        {
            return new StateConfigured(configured.Configuration);
        }
    }
}