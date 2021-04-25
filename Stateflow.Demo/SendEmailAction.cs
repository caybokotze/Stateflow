using System;
using System.Data;
using Stateflow;
using Stateflow.Demo;

namespace StateFlow.Demo
{
    public class SendEmailAction : WorkflowAction
    {

        public EmailDetails EmailDetails { get; set; }
        

        public override object GetData()
        {
            return new
            {
                EmailDetails
            };
        }

        public override void ExecuteAction()
        {
            Console.WriteLine($"Email from {EmailDetails.Email} is sending to {EmailDetails.Name}");
        }
    }
}