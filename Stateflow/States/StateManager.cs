﻿using System;
using System.Linq;
using System.Reflection;
using Stateflow.Entities;

// ReSharper disable CheckNamespace
namespace Stateflow
{
    public class StateManager
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
        }
        
        // this code will do the following:
        // - take an action as a parameter.
        // - Call GetData() on that action.
        // - create a new workflowActionEntity and persist the actionData to the db along with the workflowId.
        public void InitialiseAction(WorkflowAction workflowAction, DateTime? dateToExecute = null)
        {
            // find workflow in db.
            // get workflow in db
            var workflowName = ClassHelper.GetNameOfCallingClass();
            var action = workflowAction.GetData();
            var type = action.type;
            var serializedAction = Serializers.MessagePack.Serialize(action.obj);
            
            var actionEntity = new WorkflowActionEntity
            {
                Uuid = Guid.NewGuid(),
                Retries = 0,
                ActionBody = serializedAction,
                ActionName = type.ToString(),
                IsComplete = false,
                DateToExecute = dateToExecute,
                DateExpires = null,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
                DateProcessed = DateTime.UtcNow
            };
        }

        public void RaiseEvent(Enum eventName)
        {
            RaiseEvent(eventName.ToString());
        }

        // this code will do the following:
        // - fetch the workflow that is being executed from.
        // - fetch all the workflowStates for that workflow in the db.
        // - find the action that needs to be executed.
        // - if the action that needs to be executed matches the event that is being raised, mark for execution...
        public void RaiseEvent(string eventName)
        {
            var workflowName = ClassHelper
                .GetNameOfCallingClass();
            
            var workflowActions = StateflowDbContext
                .FetchWorkflowActionsByWorkflowName(workflowName);

            foreach (var action in workflowActions.WorkflowActionList)
            {
                if (action.ExecutionState
                    .Equals(workflowActions.WorkflowEntity.CurrentState)
                &&  action.ExecuteOnEvent
                    .Equals(eventName))
                {
                    HydrateAndExecuteAction(action);
                }
            }
        }

        private void HydrateAndExecuteAction(WorkflowActionEntity workflowActionEntity)
        {
            var action = Assembly.GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(w => w.Name.Equals(workflowActionEntity.ActionName));
            
            if (action is null)
            {
                throw new NullReferenceException();
            }

            var activatedAction = (WorkflowAction)Activator.CreateInstance(action);
            
            activatedAction?.SetData(Serializers
                .MessagePack
                .Deserialize(workflowActionEntity.ActionBody));
            
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