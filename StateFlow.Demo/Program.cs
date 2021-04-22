using System;
using System.Data;
using System.Data.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Stateflow;

namespace StateFlow.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("hi there.");
            var sqlConnection = new MySqlConnection("server=localhost;port=3306;database=workflow_test;user=sqltracking;password=sqltracking;");
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<EmailWorkflow>()
                .AddSingleton<IDbConnection>(
                    new MySqlConnection())
                .AddSingleton<IWorkflowService>(WorkflowServiceFactory
                    .Create(sqlConnection))
                .BuildServiceProvider();

            var logger = (serviceProvider
                              .GetService<ILoggerFactory>()
                          ?? throw new NullReferenceException())
                .CreateLogger<Program>();
            
            // logger.Log(LogLevel.Debug, "Testing...");

            var workflowService = serviceProvider
                .GetService<IWorkflowService>();
            
            var emailWorkflow = serviceProvider
                .GetService<EmailWorkflow>();
            
            workflowService?.InitialiseWorkflows();

            // emailWorkflow?.InitialiseAction(new SendEmailAction
            // {
            //     EmailDetails = new EmailDetails
            //     {
            //         Email = "caybokotze@gmail.com",
            //         Name = "Caybo Kotze"
            //     }
            // });
            
            // emailWorkflow?.RaiseEvent(EmailWorkflow.Events.SendEmail);

            Console.WriteLine("finished.");
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
                "workflow_test");
        }
    }
}