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
            IServiceCollection serviceCollection, 
            IServiceProvider serviceProvider,
            DatabaseProvider databaseProvider)
        {
            DbConnection = dbConnection;
            ServiceCollection = serviceCollection;
            ServiceProvider = serviceProvider;
            DatabaseProvider = databaseProvider;
        }
        
        public IDbConnection DbConnection { get; }
        public DatabaseProvider DatabaseProvider { get; }
        public IServiceCollection ServiceCollection { get; }
        public IServiceProvider ServiceProvider { get; }
    }
}