using System;
using Stateflow;

namespace StateFlow.Demo
{
    public class SendEmailAction : WorkflowAction
    {
        public EmailDetails EmailDetails { get; set; }

        public override void SetData(object obj)
        {
            EmailDetails = (EmailDetails)obj;
        }

        public override object GetData()
        {
            return new
            {
                EmailDetails
            };
        }

        public override void ExecuteAction()
        {
            Console.WriteLine("Email is sending...");
        }
    }
}