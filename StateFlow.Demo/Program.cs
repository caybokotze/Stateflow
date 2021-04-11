using System;
using System.Data;
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
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<EmailWorkflow>()
                .AddSingleton<IDbConnection>(new MySqlConnection("something here..."))
                .AddSingleton<IWorkflowService>(WorkflowServiceFactory
                    .Create(Activator
                        .CreateInstance<IDbConnection>()))
                .BuildServiceProvider();

            var logger = (serviceProvider.GetService<ILoggerFactory>() ?? throw new NullReferenceException())
                .CreateLogger<Program>();
            
            logger.Log(LogLevel.Debug, "Hi there");

            var emailWorkflow = serviceProvider
                .GetService<EmailWorkflow>();
            
            var workflowService = serviceProvider
                .GetService<WorkflowService>();
            
            emailWorkflow?.RaiseEvent(EmailWorkflow.Events.SendEmail);
            
            workflowService?.InitialiseWorkflows();
            emailWorkflow?.RegisterStates();
            emailWorkflow?.RaiseEvent("SomeEvent");
            emailWorkflow?.ForceStateOverride("Something");
            
            logger.LogDebug("eyo!");
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