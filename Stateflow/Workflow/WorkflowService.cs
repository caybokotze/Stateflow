using System;
using System.Data;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

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
            StateManagementData.CreateWorkflowTable(this);
        }

        private static string RegisterStates = "RegisterStates"; 

        private void RegisterAllStates()
        {
            var type = typeof(Workflow);
            
            // note: I'm not sure if CurrentDomain would cover the required scope of the entire application.
            // var enumerable = AppDomain.CurrentDomain.GetAssemblies()
            //     .SelectMany(s => s.GetTypes())
            //     .Where(w => type.IsAssignableFrom(w));
            
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
                        null, 
                        null);
            }
        }
    }
}