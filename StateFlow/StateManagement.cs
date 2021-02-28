using System;
using System.Data;
using System.Runtime.InteropServices.ComTypes;
using Dapper;

namespace StateFlow
{
    public abstract class StateManagement
    {
        public IWorkflowService WorkflowService { get; }

        protected enum GlobalStates
        {
            Initialise,
            Complete
        }

        public StateManagement(IWorkflowService workflowService)
        {
            WorkflowService = workflowService;
        }

        private Workflow GetWorkflow(int Id)
        {
            return WorkflowService.DbConnection
                .QueryFirst<Workflow>("SELECT * FROM stores WHERE id = @id",
                    new { id = Id });
        }

        private int SaveWorkflow(Workflow workflow)
        {
            return WorkflowService.DbConnection.QueryFirst<int>("");
        }

        private StateConfiguration RegisterState(string stateName)
        {
            IWorkflowConfiguration configuration = new WorkflowConfiguration(WorkflowService);
            var stateConfiguration = stateName switch
            {
                "Initialise" => new StateConfiguration(configuration)
                {
                    Initialised = false,
                    StateName = stateName
                },
                "Complete" => new StateConfiguration(configuration)
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