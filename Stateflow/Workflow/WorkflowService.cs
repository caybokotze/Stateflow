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
        public IServiceProvider ServiceProvider { get; }
        public string Schema { get; }
        
        public void InitialiseWorkflows()
        {
            StateflowDbContext.CreateWorkflowTable(this);
            RegisterAllStates();
        }

        private static string RegisterStates = "RegisterStates";

        private void RegisterAllStates()
        {
            // not sure if this is the best implementation or not?
            
            var type = typeof(Workflow);
            var workflows = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(w => type.IsAssignableFrom(w));
            
            foreach(var workflow in workflows)
            {
                var instance = Activator.CreateInstance(workflow);
                
                instance = (Workflow)instance;
                
                if (instance is null)
                {
                    throw new InvalidOperationException("Activator.CreateInstance returned null");
                }

                workflow
                    .InvokeMember(RegisterStates, 
                        BindingFlags.InvokeMethod | BindingFlags.Instance, 
                        null,
                        instance, null);
            }
        }
    }
}