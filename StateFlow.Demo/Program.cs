using System;
using System.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;
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

    public class EmailDetails
    {
        public string Name { get; set; }
        public string Email { get; set; }
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
            Confirmed,
            Complete
        }

        enum Events
        {
            SendEmail,
            AccountConfirmed
        }

        public override void RegisterStates()
        {
            RegisterState(GlobalState.Initialise)
                 .RegisterAction(new SendEmailAction())
                 .RaiseEventOn(Events.SendEmail)
                 .ThenChangeStateTo(States.Confirmed)
                 .SaveState();

            RegisterState(States.Confirmed)
                .RegisterAction(new SendEmailAction())
                .RaiseEventOn(Events.AccountConfirmed)
                .ThenChangeStateTo(States.Complete)
                .SaveState();
            
            RegisterState(GlobalState.Complete)
                .RegisterAction(new SendEmailAction())
                .RaiseEventOn(Events.SendEmail)
                .SaveState();
        }
    }
}