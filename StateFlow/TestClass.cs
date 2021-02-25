using System;

namespace StateFlow
{
    public class EmailWorkflow : Workflow
    {
        enum EmailWorkflowStates
        {
            Initialise,
            Sending,
            Sent,
            Opened,
            Received
        }

        enum EmailWorkFlowEvents
        {
            SendEmail,
            Retry,
            Cancel
        }

        private EmailWorkflowStates State { get; set; }

        private Action SendEmail()
        {
            return delegate
            {
                Console.WriteLine("Hi there.");
                State = EmailWorkflowStates.Sending;
            };
        }
        
        public override string Register()
        {
            RegisterState(EmailWorkflowStates.Initialise)
                .RegisterEvent(SendEmail())
                .RaiseEventOn(EmailWorkFlowEvents.SendEmail);

            return EmailWorkflowStates.Initialise.ToString();
        }
    }
}