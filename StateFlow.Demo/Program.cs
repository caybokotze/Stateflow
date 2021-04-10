using System;
using System.Data;
using System.Runtime.CompilerServices;
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

            var emailWorkflow = serviceProvider.GetService<EmailWorkflow>();
            
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
    
    public class SendEmailAction : WorkflowAction
    {
        public override void ExecuteAction()
        {
            Console.WriteLine("Email is sending...");
        }
    }

    public class EmailWorkflow : Workflow
    {
        public EmailWorkflow(IWorkflowService workflowService)
            : base(workflowService)
        {
        }

        public enum States
        {
            Initialise,
            Complete
        }

        enum Events
        {
            SendEmail
        }

        public override void RegisterStates()
        {
            RegisterState(GlobalStates.Initialise)
                 .RegisterAction(new SendEmailAction())
                 .RaiseEventOn(Events.SendEmail)
                 .ThenChangeStateTo(States.Complete)
                 .SaveState();
        }
    }
}