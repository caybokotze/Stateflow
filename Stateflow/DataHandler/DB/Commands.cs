using System;
using System.Linq;
using Dapper;
using Stateflow.Entities;

// ReSharper disable once CheckNamespace
namespace Stateflow
{
    public static partial class StateflowDbContext
    {
        public static class Commands
        {
            public static int CreateWorkflowState(
                IWorkflowService workflowService, 
                WorkflowState workflowState)
            {
                return workflowService.DbConnection
                    .Query<int>(CommandBuilder
                            .CreateOrUpdateWorkflowState(),
                        workflowState)
                    .FirstOrDefault();
            }
            
            public static int CreateOrUpdateWorkflow(
                IWorkflowService workflowService, 
                WorkflowEntity workflowEntity)
            {
                return workflowService.DbConnection
                    .Query<int>(CommandBuilder.CreateOrUpdateWorkflow(workflowEntity),
                        workflowEntity)
                    .FirstOrDefault();
            }
            
            public static int CreateOrUpdateWorkflowAction(
                IWorkflowService workflowService, 
                WorkflowActionEntity workflowActionEntity)
            {
                return workflowService.DbConnection
                    .Query<int>(CommandBuilder.CreateOrUpdateWorkflowAction(workflowActionEntity), 
                        workflowActionEntity)
                    .FirstOrDefault();
            }

            public static void DeleteWorkflowByUuid(
                IWorkflowService workflowService, 
                Guid uuid)
            {
                workflowService
                    .DbConnection
                    .Query(CommandBuilder.DeleteWorkflowByUuid(),
                    new { Uuid = uuid});
            }

            public static void DeleteWorkflowActionsByWorkflowUuid(
                IWorkflowService workflowService, 
                Guid uuid)
            {
                workflowService
                    .DbConnection
                    .Query(CommandBuilder.DeleteWorkflowActionsByWorkflowUuid(),
                        new { Uuid = uuid });
            }

            public static void DeleteWorkflowStatesByWorkflowUuid(
                IWorkflowService workflowService,
                Guid uuid)
            {
                workflowService
                    .DbConnection
                    .Query(CommandBuilder.DeleteWorkflowStatesByWorkflowUuid(),
                        new { Uuid = uuid });
            }
        }
    }
}