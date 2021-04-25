# Stateflow
Stateflow is a state management solution to assist with complex workflows and allows you as the developer to leverage the power of persistent machine-states to create more robust software solutions that fail softly. 😉

# Notes:
- Currently only supports MySql
- No RabbitMq integration as yet.

# Diagram

[Stateflow High Level](https://user-images.githubusercontent.com/25146896/116010400-fc79c300-a61e-11eb-8382-8beb6cccce1f.png)


# Setup
- Install MySql

RUN DEMO PROJECTS AS IS:
```mysql
CREATE SCHEMA workflow_test;
USE SCHEMA workflow_test;
CREATE USER 'sqltracking'@'localhost' IDENTIFIED BY 'sqltracking';
GRANT ALL PRIVILEGES ON *  TO 'sqltracking'@'localhost';
```

# State management solutions

Although there are not currently a lot of state management solutions around, the problem they try and solve is simply this; *to maintain state for a given action over time.*

The point of stateflow is to create a really straight forward solution with the following phiolophies in mind, actions perform actions, workflows define rules and actions should be self contained things.

# Some concepts to outline

## Workflows define rules

As outlined in the example below, the main point of a workflow should only be to define action and event rules. "This should happen under this condition, etc, etc..."

```csharp

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
            .RegisterAction<SendEmailAction>()
            .ExecuteActionOnEvent(Events.SendEmail)
            .ThenChangeStateTo(States.Confirmed);

        RegisterState(States.Confirmed)
            .RegisterAction<SendEmailAction>()
            .ExecuteActionOnEvent(Events.AccountConfirmed)
            .ThenChangeStateTo(States.Complete);

        RegisterState(GlobalState.Complete)
            .RegisterAction<SendEmailAction>()
            .ExecuteActionOnEvent(Events.SendEmail);
    }
}

```

To keep things simple, workflows should only define the rules under which actions should be performed. They should not resolve any additional dependencies as you see in the example above.

## Actions perform actions *(duh)*

The main role of an action is to perform the intented action and nothing more. Actions are also -- for now at least -- not resolvable. You can not resolve any dependencies within an action. The only thing that can be persisted within an action are the properties that you define.

## Actions are self-contained

Adding onto the point above. Actions are self-contained. They only contain the values you initialise them with and that instance of that action, will forever remian that way until executed. Every time something needs to be done, you would use an action to do that thing.

Actions also have a GetData() method. This is so that the developer is aware that they need to explicitly define what should be persisted to the database and what doesn't have to be.

```csharp

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

```

## Workflows should not change once defined

One thing to keep in mind is that actions and thier states are dependent on keeping workflow definitions more or less the same. This also includes the workflow name itself. If you change the name of a workflow, do keep in mind it would be as if all the actions that depend on that previous workflow no longer exist.

## Initialisation does not equal execution

When an action gets initialised, it is not yet executed. It is the job of some service or worker to pick up that a new action exists within the database and needs to be executed under the conditions that you have specified in the workflow. They will not just execute immediately. There will always be a slight delay, sometimes a few seconds or minutes or hours -- depending on whether or not you have added a delay.

## Actions are initialised like this

After an action is initialised, everything you used to initialise it with (and what you define the action to save) will be persisted to the database. Then the service / worker picks up the action it will execute the ExecuteAction method.

```csharp
 workflowService?.InitialiseAction<EmailWorkflow>(new SendEmailAction
{
    EmailDetails = new EmailDetails
    {
        Email = "caybokotze@gmail.com",
        Name = "Caybo Kotze"
    }
}, DateTime.Now.AddDays(1))
    .OnWorkflowEvent(EmailWorkflow.Events.AccountConfirmed)
    .OnWorkflowState(EmailWorkflow.States.Complete);
```
