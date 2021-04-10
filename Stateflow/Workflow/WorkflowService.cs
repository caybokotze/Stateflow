using System;
using System.Data;
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
    }
}