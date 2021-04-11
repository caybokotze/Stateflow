using System;

// ReSharper disable CheckNamespace
namespace Stateflow
{
    public static class EventConfigurationHelpers
    {
        public static EventConfigured ExecuteActionOnEvent(
            this StateConfigured stateConfigured, 
            string eventName)
        {
            stateConfigured
                .StateConfiguration
                .CurrentStateConfiguration
                .RaiseOnEvent = eventName;
            
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