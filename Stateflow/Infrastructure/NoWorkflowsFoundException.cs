using System;

namespace Stateflow
{
    public class NoWorkflowsFoundException : Exception
    {
        public NoWorkflowsFoundException() : base("There were no workflows found in the executing assembly...")
        {
            
        }
    }
}