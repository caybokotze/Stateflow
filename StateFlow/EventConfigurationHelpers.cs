using System;
using System.Runtime.CompilerServices;

namespace StateFlow
{
    public static class EventConfigurationHelpers
    {
        public static EventConfigured RaiseEventOn(this StateConfigured configured, string eventName)
        {
            return new EventConfigured(configured.Configuration);
        }
        
        public static EventConfigured RaiseEventOn(this StateConfigured configured, Enum eventName)
        {
            return RaiseEventOn(configured, eventName.ToString());
        }
    }
}