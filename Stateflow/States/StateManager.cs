using System;
using Dapper;
using SqExpress.SqlExport;
using static SqExpress.SqQueryBuilder;

// ReSharper disable CheckNamespace
namespace Stateflow
{
    public abstract class StateManager
    {
        private IWorkflowService WorkflowService { get; }
        
        protected static class GlobalStates
        {
            public const string Initialise = "Initialise";
            public const string Complete = "Complete";
        }

        protected StateManager(IWorkflowService workflowService)
        {
            WorkflowService = workflowService;
        }

        private Workflow GetWorkflow(ulong id)
        {
            return WorkflowService.DbConnection
                .QueryFirst<Workflow>("SELECT * FROM stores WHERE id = @id",
                    new { id = id });
        }

        private int SaveWorkflow(Workflow workflow)
        {
            var query = Select("Hi there.").Done();
            var result = MySqlExporter.Default.ToSql(query);
            return WorkflowService.DbConnection.QueryFirst<int>(result);
        }

        protected StateConfiguration RegisterState(string stateName)
        {
            IWorkflowConfiguration configuration = new WorkflowConfiguration(WorkflowService);
            var stateConfiguration = stateName switch
            {
                GlobalStates.Initialise => new StateConfiguration(configuration)
                {
                    Initialised = false,
                    StateName = stateName
                },
                GlobalStates.Complete => new StateConfiguration(configuration)
                {
                  Initialised  = true,
                  StateName = stateName,
                  Complete = true
                },
                _ => new StateConfiguration(configuration)
                {
                    Initialised = true,
                    StateName = stateName
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