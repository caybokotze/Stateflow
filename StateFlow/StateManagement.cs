using System;

namespace StateFlow
{
    public abstract class StateManagement
    {
        protected enum GlobalStates
        {
            Initialise,
            Complete
        }

        private StateConfiguration RegisterState(string stateName)
        {
            var stateConfiguration = stateName switch
            {
                "Initialise" => new StateConfiguration()
                {
                    Initialised = false,
                    StateName = stateName
                },
                "Complete" => new StateConfiguration()
                {
                  Initialised  = true,
                  StateName = stateName
                },
                _ => new StateConfiguration()
                {
                    Initialised = true,
                    StateName = stateName
                }
            };
            return stateConfiguration;
        }
        
        protected StateConfiguration RegisterState(Enum stateName)
        {
            return RegisterState(stateName.ToString());
        }
    }

    public static class WorkflowExtensions
    {
        public static void Dothis(this Workflow workflow, string something)
        {
            
        }
    }
}