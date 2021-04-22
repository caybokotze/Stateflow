using System;

// ReSharper disable once CheckNamespace
namespace Stateflow
{
    public class ConstructorParametersNotDefined : Exception
    {
        public ConstructorParametersNotDefined() : base("Workflows can only contain types of `WorkflowService` in the constructor.")
        {
            
        }
    }
}