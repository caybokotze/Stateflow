using System;
using System.Linq;
using System.Reflection;
using Stateflow.Entities;

// ReSharper disable CheckNamespace
namespace Stateflow
{
    public abstract class StateManager
    {
        private IWorkflowService WorkflowService { get; }
        private IWorkflowConfiguration WorkflowConfiguration { get; }
        
        protected static class GlobalState
        {
            public const string Initialise = "Initialise";
            public const string Complete = "Complete";
        }

        protected StateManager(IWorkflowService workflowService)
        {
            WorkflowService = workflowService;
            WorkflowConfiguration = new WorkflowConfiguration(workflowService);
        }

        public void ForceStateOverride(string stateName)
        {
            // implement some logic here...
            return;
        }

        protected void RaiseEvent(string eventName)
        {
            var workflowName = ClassHelper.GetNameOfCallingClass();
            var workflowActions = StateManagementData.FetchWorkflowActionsByWorkflowName(workflowName);

            foreach (var action in workflowActions.WorkflowActionList)
            {
                if (action.ExecutionState
                    .Equals(workflowActions.WorkflowEntity.CurrentState)
                &&  action.ExecuteOnEvent
                    .Equals(eventName))
                {
                    InvokeAction(action);
                }
            }
        }

        private void InvokeAction(WorkflowActionEntity workflowActionEntity)
        {
            var action = Assembly.GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(w => w.Name.Equals(workflowActionEntity.ActionName));
            
            if (action is null)
            {
                throw new NullReferenceException();
            }

            var activatedAction = (WorkflowAction)Activator.CreateInstance(action);
            activatedAction?.SetData(workflowActionEntity.ActionBody);
            activatedAction?.ExecuteAction();
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