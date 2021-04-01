// ReSharper disable CheckNamespace

using System;
using System.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Stateflow
{
    public static class DefaultDependencyHelper
    {
        public static IServiceCollection Configure(
            this IServiceCollection collection,
            IDbConnection databaseConnection,
            IServiceProvider serviceProvider,
            DatabaseProvider databaseProvider)
        {
            collection.AddTransient<IWorkflowService, WorkflowService>();
            
            collection.AddTransient<WorkflowService>(ws => 
                new WorkflowService(
                    databaseConnection,
                    collection,
                    serviceProvider,
                    databaseProvider));
            
            return collection;
        }
    }
}