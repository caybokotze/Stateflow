using System;
using System.Security.Authentication.ExtendedProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace StateFlow.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("hi there.");
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .RegisterStateflow()
                .BuildServiceProvider();

            var logger = (serviceProvider.GetService<ILoggerFactory>() ?? throw new NullReferenceException())
                .CreateLogger<Program>();
            
            logger.Log(LogLevel.Debug, "Hi there");

            var emailWorkflow = serviceProvider.GetService<Workflow>();
            emailWorkflow?.Register();
            logger.LogDebug("eyo!");
            Console.WriteLine("finished.");
        }
    }

    public interface IEmailWorkflow
    {
        string Register();
    }

    public class EmailWorkflow : Workflow, IEmailWorkflow
    {
        public EmailWorkflow(IServiceProvider provider) : base(provider)
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
            Console.WriteLine("Register has been triggered.");
            RegisterState(GlobalStates.Initialise)
                 .RegisterEvent(SendEmail())
                 .RaiseEventOn(Events.SendEmail).ThenChangeStateTo(States.Complete);
            //
            // this.Dothis("");
            return GlobalStates.Initialise.ToString();
            
        }
    }
}