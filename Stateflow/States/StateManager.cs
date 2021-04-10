using System;
using System.ComponentModel;
using System.Linq;
using Dapper;
using SqExpress.SqlExport;
using Stateflow.Entities;
using static SqExpress.SqQueryBuilder;

// ReSharper disable CheckNamespace
namespace Stateflow
{
    public abstract class StateManager
    {
        private static class Constants
        {
            public const string WorkflowTableName = "workflows";
            public const string WorkflowActionTableName = "workflow_actions";
        }
        
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
            return;
        }

        protected void RaiseEvent(string eventName)
        {
            
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
                StateName = stateName,
                WorkflowType = ClassHelper.GetNameOfCallingClass()
            };

            PersistWorkflow(workflow);

            return new StateConfiguration(WorkflowConfiguration);
        }

        private int PersistWorkflow(WorkflowEntity workflow)
        {
            return WorkflowService
                .DbConnection
                .Query<int>(QueryBuilder
                        .CreateTableStatement<WorkflowEntity>(WorkflowService.DatabaseProvider,
                            new DbExecutionContext(Constants.WorkflowTableName, WorkflowService.Schema)),
                    workflow)
                .FirstOrDefault();
        }
        
        protected StateConfiguration RegisterState(Enum stateName)
        {
            Console.WriteLine(stateName);
            return RegisterState(stateName.ToString());
        }
    }
}