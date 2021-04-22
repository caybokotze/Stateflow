using System;
using System.Linq;
using Dapper;
using Stateflow.Entities;

// ReSharper disable CheckNamespace
namespace Stateflow
{
    public static partial class StateflowDbContext
    {
        public static class Queries
        {
            public static WorkflowEntity FetchWorkflowByName(IWorkflowService workflowService, string name)
            {
                return workflowService
                    .DbConnection
                    .Query<WorkflowEntity>("SELECT * from workflows WHERE workflow_name = @Name;", 
                        new { Name = name})
                    .FirstOrDefault();
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