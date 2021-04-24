using System;
using System.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Stateflow;
using Stateflow.Demo;

namespace StateFlow.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = InitialiseApplication();

            var workflowService = serviceProvider
                .GetService<IWorkflowService>();

            workflowService?.InitialiseWorkflows();
            
            // keep in mind each time this is run a new action is created in the db.
            workflowService?.InitialiseAction<EmailWorkflow>(new SendEmailAction
            {
                EmailDetails = new EmailDetails
                {
                    Email = "caybokotze@gmail.com",
                    Name = "Caybo Kotze"
                }
            }, DateTime.Now.AddDays(1))
                .OnWorkflowEvent(EmailWorkflow.Events.AccountConfirmed)
                .OnWorkflowState(EmailWorkflow.States.Complete);
            
            Console.WriteLine("finished.");
        }


        static ServiceProvider InitialiseApplication()
        {
            var sqlConnection = new MySqlConnection("server=localhost;port=3306;database=workflow_test;user=sqltracking;password=sqltracking;");
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<EmailWorkflow>()
                .AddSingleton<IDbConnection>(sqlConnection)
                .AddSingleton<IWorkflowService>(WorkflowServiceFactory
                    .Create(sqlConnection))
                .BuildServiceProvider();

            var logger = (serviceProvider
                              .GetService<ILoggerFactory>()
                          ?? throw new NullReferenceException())
                .CreateLogger<Program>();

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
                null, 
                DatabaseProvider.MySql, 
                false);
        }
    }
}