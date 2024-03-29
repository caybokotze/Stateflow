﻿using System;

namespace Stateflow
{
    public static class WorkflowServiceExtensions {
        public static ActionInitialising OnWorkflowEvent(this ActionInitialising actionInitialising, string eventName)
        {
            actionInitialising.WorkflowActionEntity.ActionEvent = eventName;
            return actionInitialising;
        }
        
        public static ActionInitialising OnWorkflowEvent(this ActionInitialising actionInitialising, Enum eventName)
        {
            return OnWorkflowEvent(actionInitialising, eventName.ToString());
        }

        public static void OnWorkflowState(this ActionInitialising actionInitialising, string stateName)
        {
            actionInitialising.WorkflowActionEntity.ActionState = stateName;
            var actionEntity = actionInitialising.WorkflowActionEntity;
            var workflowService = actionInitialising.WorkflowService;
            
            StateflowDbContext.Commands.CreateOrUpdateWorkflowAction(
                workflowService, 
                actionEntity);
        }

        public static void OnWorkflowState(this ActionInitialising actionInitialising, Enum stateName)
        {
            OnWorkflowState(actionInitialising, stateName.ToString());
        }
    }
}