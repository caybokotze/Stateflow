﻿using System;
using System.Linq;
using System.Reflection;
using Dapper;
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
            var workflowName = ClassHelper.GetNameOfCallingClass();
            var workflow = StateflowDbContext.Queries.FetchWorkflowByName(this.WorkflowService, workflowName);
            var action = workflowAction.GetData();
            var type = action.type;
            var serializedAction = Serializers.MessagePack.Serialize(action.obj);
            
            var actionEntity = new WorkflowActionEntity
            {
                Uuid = Guid.NewGuid(),
                WorkflowUuid = workflow.Uuid,
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
        // - find the actions in workflowStates that is the same as the eventName.
        // - find that specific action in the db and mark for execution.
        
        public void RaiseEvent(string eventName)
        {
            var workflowName = ClassHelper
                .GetNameOfCallingClass();
            
            var workflowActions = StateflowDbContext
                .Queries.FetchWorkflowActionsByWorkflowName(workflowName);

            foreach (var action in workflowActions.WorkflowActionList)
            {
                // if (action.ExecutionState
                //     .Equals(workflowActions.WorkflowEntity.CurrentState)
                // &&  action.ExecuteOnEvent
                //     .Equals(eventName))
                // {
                //     HydrateAndExecuteAction(action);
                // }
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