using System;
using System.ComponentModel;
using Dapper;
using SqExpress.SqlExport;
using static SqExpress.SqQueryBuilder;

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
            return;
        }

        protected void RaiseEvent(string eventName)
        {
            
        }

        private ulong SaveWorkflow(Workflow workflow)
        {
            var query = Select("Hi there.").Done();
            var result = MySqlExporter.Default.ToSql(query);
            return WorkflowService.DbConnection.QueryFirst<ulong>(result);
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
            var stateConfiguration = stateName switch
            {
                GlobalStates.Initialise => new StateConfiguration(WorkflowConfiguration)
                {
                    Initialised = false,
                    StateName = stateName
                },
                GlobalStates.Complete => new StateConfiguration(WorkflowConfiguration)
                {
                  Initialised  = true,
                  StateName = stateName,
                  Complete = true
                },
                _ => new StateConfiguration(WorkflowConfiguration)
                {
                    Initialised = true,
                    StateName = stateName,
                    CurrentStateConfiguration = new StateConfiguration.RegisteredState()
                    {
                        CurrentState = stateName
                    }
                }
            };
            return stateConfiguration;
        }
        
        protected StateConfiguration RegisterState(Enum stateName)
        {
            Console.WriteLine(stateName);
            return RegisterState(stateName.ToString());
        }
    }
}