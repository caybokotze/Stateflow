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
                .CurrentState
                .RegisteredEvent = eventName;
            
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