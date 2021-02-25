using System;
using System.Security.Authentication.ExtendedProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace StateFlow.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("hi there.");
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IEmailWorkflow, EmailWorkflow>()
                .BuildServiceProvider();

            var logger = (serviceProvider.GetService<ILoggerFactory>() ?? new LoggerFactory())
                .CreateLogger<Program>();
            
            logger.LogDebug("starting application");

            var bar = serviceProvider.GetService<IEmailWorkflow>();
            bar?.Register();
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

        public override string Register()
        {
            Console.WriteLine("Register has been triggered.");

            //
            // RegisterState("")
            //     .RegisterEvent(SendEmail()).RaiseEventOn("").ThenChangeStateTo("");
            return "";
        }
    }
}