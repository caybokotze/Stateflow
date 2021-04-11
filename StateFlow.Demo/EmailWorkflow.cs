using Org.BouncyCastle.X509.Extension;
using Stateflow;

namespace StateFlow.Demo
{
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

        public enum Events
        {
            SendEmail,
            AccountConfirmed
        }

        public override void RegisterStates()
        {
            RegisterState(GlobalState.Initialise)
                .RegisterAction(new SendEmailAction(this.WorkflowService))
                .ExecuteActionOnEvent(Events.SendEmail)
                .ThenChangeStateTo(States.Confirmed)
                .SaveState();

            RegisterState(States.Confirmed)
                .RegisterAction(new SendEmailAction(this.WorkflowService))
                .ExecuteActionOnEvent(Events.AccountConfirmed)
                .ThenChangeStateTo(States.Complete)
                .SaveState();
            
            RegisterState(GlobalState.Complete)
                .RegisterAction(new SendEmailAction(this.WorkflowService))
                .ExecuteActionOnEvent(Events.SendEmail)
                .SaveState();
        }
    }
}