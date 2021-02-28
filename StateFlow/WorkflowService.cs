using System;
using System.Data;
using Microsoft.Extensions.DependencyInjection;

namespace StateFlow
{
    public class WorkflowService : IWorkflowService
    {
        public WorkflowService(
            IDbConnection dbConnection, 
            IServiceCollection serviceCollection, 
            IServiceProvider serviceProvider)
        {
            DbConnection = dbConnection;
            ServiceCollection = serviceCollection;
            ServiceProvider = serviceProvider;
        }
        
        public IDbConnection DbConnection { get; }
        public IServiceCollection ServiceCollection { get; }
        public IServiceProvider ServiceProvider { get; }
    }
}