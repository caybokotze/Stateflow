// ReSharper disable CheckNamespace

using System;
using Stateflow.Entities;

namespace Stateflow
{
    public static partial class StateflowDbContext
    {
        public static class Queries
        {
            public static WorkflowEntity FetchWorkflowByName(string name)
            {
                return new WorkflowEntity();
            }

            public static WorkflowActionEntity FetchWorkflowEntityByUuid(Guid id)
            {
                return new WorkflowActionEntity();
            }

            public static WorkflowActions FetchWorkflowActionsByWorkflowUuid(Guid id)
            {
                return new WorkflowActions();
            }
        
            public static WorkflowActions FetchWorkflowActionsByWorkflowName(string name)
            {
                return new WorkflowActions();
            }
        }
    }
}