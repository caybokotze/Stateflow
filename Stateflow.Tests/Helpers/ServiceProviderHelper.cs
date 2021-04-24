using System.Data;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using Stateflow.Demo;

namespace Stateflow.Tests.Helpers
{
    public class ServiceProviderHelper
    {
        public static ServiceProvider BuildServiceProvider()
        {
            var sqlConnection = new MySqlConnection("server=localhost;port=3306;database=workflow_test;user=sqltracking;password=sqltracking;");
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IWorkflowService>(WorkflowServiceFactory
                    .Create(sqlConnection))
                .BuildServiceProvider();
            return serviceProvider;
        }
    }
    
    public class WorkflowServiceFactory
    {
        public static WorkflowService Create(
            IDbConnection dbConnection)
        {
            return new WorkflowService(dbConnection, 
                null)
            {
                DatabaseProvider = DatabaseProvider.MySql
            };
        }
    }
}