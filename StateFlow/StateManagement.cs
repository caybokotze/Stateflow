using System;

namespace StateFlow
{
    public abstract class StateManagement
    {
        protected StateConfiguration RegisterState(string stateName)
        {
            return new StateConfiguration();
        }
        
        protected StateConfiguration RegisterState(Enum stateName)
        {
            return new StateConfiguration();
        }
    }
}