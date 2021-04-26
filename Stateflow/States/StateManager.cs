using System;
using Stateflow.Entities;

// ReSharper disable CheckNamespace
namespace Stateflow
{
    public class StateManager
    {
        private IWorkflowService WorkflowService { get; }
        public string CurrentWorkflowName { get; set; }

        public static class GlobalState
        {
            public const string Initialise = "Initialise";
            public const string Complete = "Complete";
        }

        protected StateManager(IWorkflowService workflowService)
        {
            WorkflowService = workflowService;
        }

        protected StateConfiguration RegisterState(string stateName)
        {
            CurrentWorkflowName ??= ClassHelper.GetNameOfCallingClass();
            
            var workflow = StateflowDbContext
                .Queries
                .FetchWorkflowByName(WorkflowService, CurrentWorkflowName);

            if (workflow is null)
            {
                throw new NullReferenceException("The workflow configured for this state can not be found");
            }
            
            return new StateConfiguration(WorkflowService)
            {
                CurrentState = new WorkflowState
                {
                    RegisteredState = stateName,
                    WorkflowUuid = workflow.Uuid
                }
            };
        }
        
        protected StateConfiguration RegisterState(Enum stateName)
        {
            CurrentWorkflowName ??= ClassHelper.GetNameOfCallingClass();
            
            Console.WriteLine(stateName);
            return RegisterState(stateName.ToString());
        }
    }
    
}