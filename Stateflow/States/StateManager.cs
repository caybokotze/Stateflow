using System;
using Dapper;
using Stateflow.Entities;

// ReSharper disable CheckNamespace
namespace Stateflow
{
    public abstract class StateManager
    {
        private IWorkflowService WorkflowService { get; }
        private IWorkflowConfiguration WorkflowConfiguration { get; }
        
        protected static class GlobalStates
        {
            public const string Initialise = "Initialise";
            public const string Complete = "Complete";
        }

        protected StateManager(IWorkflowService workflowService)
        {
            WorkflowService = workflowService;
            WorkflowConfiguration = new WorkflowConfiguration(workflowService);
        }

        private Workflow GetWorkflow(ulong id)
        {
            return WorkflowService.DbConnection
                .QueryFirst<Workflow>("SELECT * FROM stores WHERE id = @id",
                    new { id = id });
        }

        public void ForceStateOverride(string stateName)
        {
            // implement some logic here...
            return;
        }

        protected void RaiseEvent(string eventName)
        {
            // implement some logic here...
            return;
        }

        protected StateConfiguration DisposeState(string stateName)
        {
            return new StateConfiguration(WorkflowConfiguration);
        }
        
        protected StateConfiguration DisposeState(Enum stateName)
        {
            return DisposeState(stateName.ToString());
        }

        protected StateConfiguration RegisterState(string stateName)
        {
            var workflow = new WorkflowEntity
            {
                CurrentState = stateName,
                WorkflowName = ClassHelper.GetNameOfCallingClass()
            };

            return new StateConfiguration(WorkflowConfiguration)
            {
                Initialised = true,
                CurrentStateConfiguration =
                {
                    CurrentState = stateName
                }
            };
        }
        
        protected StateConfiguration RegisterState(Enum stateName)
        {
            Console.WriteLine(stateName);
            return RegisterState(stateName.ToString());
        }
    }
    
}