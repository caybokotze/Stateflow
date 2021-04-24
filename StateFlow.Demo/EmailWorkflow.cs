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
            Confirmed,
            Complete
        }

        public enum Events
        {
            SendEmail,
            AccountConfirmed
        }

        public override void DefineWorkflowRules()
        {
            RegisterState(GlobalState.Initialise)
                .RegisterAction(new SendEmailAction())
                .ExecuteActionOnEvent(Events.SendEmail)
                .ThenChangeStateTo(States.Confirmed);

            RegisterState(States.Confirmed)
                .RegisterAction(new SendEmailAction())
                .ExecuteActionOnEvent(Events.AccountConfirmed)
                .ThenChangeStateTo(States.Complete);

            RegisterState(GlobalState.Complete)
                .RegisterAction(new SendEmailAction())
                .ExecuteActionOnEvent(Events.SendEmail);
        }
    }
}