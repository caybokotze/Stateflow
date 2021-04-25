using System.Data;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using Stateflow.Demo;
using StateFlow.Demo;

namespace Stateflow.ServiceDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = InitialiseApplication();

            var workflowService = serviceProvider.GetService<IWorkflowService>();
            
            var activeActions = workflowService?
                .LoadActiveActionsForWorkflow<EmailWorkflow>();
            
            var action = workflowService?
                .LoadAction<SendEmailAction>(activeActions[0]);
            
            action?.ExecuteAction();
        }
        
        
        static ServiceProvider InitialiseApplication()
        {
            var sqlConnection = new MySqlConnection("server=localhost;port=3306;database=workflow_test;user=sqltracking;password=sqltracking;");
            var serviceProvider = new ServiceCollection()
                .AddSingleton<EmailWorkflow>()
                .AddSingleton<IDbConnection>(sqlConnection)
                .AddSingleton<IWorkflowService>(WorkflowServiceFactory
                    .Create(sqlConnection))
                .BuildServiceProvider();

            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            
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