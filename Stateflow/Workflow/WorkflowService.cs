using System;
using System.Data;
using System.Linq;
using System.Reflection;
using Stateflow.Entities;

// ReSharper disable CheckNamespace
namespace Stateflow
{
    public class WorkflowService : IWorkflowService
    {
        public WorkflowService(
            IDbConnection dbConnection,
            IServiceProvider serviceProvider,
            DatabaseProvider databaseProvider, string schema)
        {
            DbConnection = dbConnection;
            ServiceProvider = serviceProvider;
            DatabaseProvider = databaseProvider;
            Schema = schema;
        }
        
        public IDbConnection DbConnection { get; }
        public DatabaseProvider DatabaseProvider { get; }
        public string Schema { get; }
        public IServiceProvider ServiceProvider { get; }
        
        public void InitialiseWorkflows()
        {
            var workflows =  ReflectiveEnumerator
                .GetEnumerableOfEntryType<Workflow>()
                .ToList();

            if (!workflows.Any()) throw new NoWorkflowsFoundException();

            foreach (var workflow in workflows)
            {
                object typeInstance;
                try
                {
                    typeInstance = Activator.CreateInstance(workflow, this);
                }
                catch
                {
                    throw new ConstructorParametersNotDefined();
                }

                var workflowInstance = (Workflow) typeInstance;
                
                InitialiseWorkflow(workflowInstance?.GetType().Name);

                if (typeInstance is null)
                {
                    throw new InvalidOperationException("Workflow Service tried to initialise an invalid workflow.");
                }

                workflowInstance.RegisterStates();
            }
        }

        private void InitialiseWorkflow(string workflowName)
        {
            if (workflowName is null)
            {
                throw new NullReferenceException("The workflow specified is null");
            }
            
            var workflow = new WorkflowEntity
            {
                CurrentState = StateManager.GlobalState.Initialise,
                Uuid = Guid.NewGuid(),
                IsActive = true,
                WorkflowName = workflowName,
                DateCreated = DateTime.UtcNow
            };
            
            StateflowDbContext
        }

        public void DisposeWorkflow(Guid workflowUuid)
        {
            // todo: Code that deletes a workflow, workflow_states and workflow_actions...
        }

        private static string RegisterStates = "RegisterStates";
    }
}