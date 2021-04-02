using System;

// ReSharper disable CheckNamespace
namespace Stateflow
{
    public static class EventConfigurationHelpers
    {
        public static EventConfigured RaiseEventOn(this StateConfigured configured, string eventName)
        {
            return new EventConfigured(configured.Configuration);
        }
        
        public static EventConfigured RaiseEventOn(
            this StateConfigured configured,
            Enum eventName)
        {
            return RaiseEventOn(configured, eventName.ToString());
        }
    }
}