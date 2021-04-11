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

        public override (object obj, Type type) GetData()
        {
            return (new
            {
                EmailDetails
            }, GetType());
        }

        public override void ExecuteAction()
        {
            Console.WriteLine("Email is sending...");
        }

        public SendEmailAction(IWorkflowService workflowService) : base(workflowService)
        {
        }
    }
}