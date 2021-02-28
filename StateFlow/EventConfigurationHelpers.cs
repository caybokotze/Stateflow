using System;

namespace StateFlow
{
    public static class EventConfigurationHelpers
    {
        public static EventConfigured RaiseEventOn(this StateConfigured configured, string eventName)
        {
            return new EventConfigured();
        }
        
        public static EventConfigured RaiseEventOn(this StateConfigured configured, Enum eventName)
        {
            return new EventConfigured
            {

            };
        }
    }
}