using System;

namespace StateFlow
{
    public static class StateConfigurationHelpers
    {
        public static StateConfigured RegisterEvent<T>(this StateConfiguration configuration) where T : IRegisteredEvent
        {
            return new StateConfigured();
        }

        public static StateConfigured RegisterEvent(this StateConfiguration configuration, Action action)
        {
            return new StateConfigured();
        }

        public static StateConfigured ThenChangeStateTo(this EventConfigured configured, string stateName)
        {
            return new StateConfigured();
        }
        
        public static StateConfigured ThenChangeStateTo(this EventConfigured configured, Enum stateName)
        {
            return new StateConfigured();
        }
    }
}