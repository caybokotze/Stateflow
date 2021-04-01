using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
                .BuildServiceProvider();

            var logger = (serviceProvider.GetService<ILoggerFactory>() ?? throw new NullReferenceException())
                .CreateLogger<Program>();
            
            logger.Log(LogLevel.Debug, "Hi there");

            var emailWorkflow = serviceProvider.GetService<EmailWorkflow>();
            emailWorkflow?.Register();
            logger.LogDebug("eyo!");
            Console.WriteLine("finished.");
        }
    }

    public class EmailWorkflow : Workflow
    {
        public EmailWorkflow(IWorkflowService workflowService)
            : base(workflowService)
        {
        }
        
        private Action SendEmail()
        {
            return delegate
            {
                Console.WriteLine("");
            };
        }

        enum States
        {
            Initialise,
            Complete
        }

        enum Events
        {
            SendEmail
        }

        public override string Register()
        {
            RegisterState(GlobalStates.Initialise)
                 .RegisterEvent(SendEmail())
                 .RaiseEventOn(Events.SendEmail)
                 .ThenChangeStateTo(States.Complete);
            
            RegisterState(GlobalStates.Initialise)
                .RegisterEvent(SendEmail())
                .RaiseEventOn(Events.SendEmail)
                .ThenChangeStateTo(States.Complete);
            
            RegisterState(GlobalStates.Initialise)
                .RegisterEvent(SendEmail())
                .RaiseEventOn(Events.SendEmail)
                .ThenChangeStateTo(States.Complete);
            
            RegisterState(GlobalStates.Initialise)
                .RegisterEvent(SendEmail())
                .RaiseEventOn(Events.SendEmail)
                .ThenChangeStateTo(States.Complete);
            
            return GlobalStates.Initialise.ToString();
        }
    }
}