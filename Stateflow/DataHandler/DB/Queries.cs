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
                    .Query<WorkflowEntity>(QueryBuilder.FetchWorkflowByName(), 
                        new { Name = name})
                    .FirstOrDefault();
            }

            public static WorkflowActionEntity FetchWorkflowActionByUuid(
                IWorkflowService workflowService,
                Guid uuid)
            {
                return workflowService
                    .DbConnection
                    .Query<WorkflowActionEntity>(QueryBuilder.FetchWorkflowActionByUuid(), new
                    {
                        Uuid = uuid
                    })
                    .FirstOrDefault();
            }

            public static WorkflowActionEntity[] FetchActiveActionsByWorkflowGuid(
                IWorkflowService workflowService,
                Guid workflowUuid)
            {
                return workflowService
                    .DbConnection
                    .Query<WorkflowActionEntity>(QueryBuilder.FetchActiveActionsByWorkflowUuid(), new
                    {
                        WorkflowUuid = workflowUuid
                    })
                    .ToArray();
            }
            
            
        }
    }
}