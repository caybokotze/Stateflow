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