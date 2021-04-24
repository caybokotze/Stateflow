using System;
using System.Data;
using Stateflow.Entities;

// ReSharper disable CheckNamespace
namespace Stateflow
{
    public interface IWorkflowService
    {
        IDbConnection DbConnection { get; }
        DatabaseProvider DatabaseProvider { get; }
        IServiceProvider ServiceProvider { get; }
        string Schema { get; }
        void InitialiseWorkflows();
        void DisposeWorkflow<T>() where T : Workflow;
        ActionInitialising InitialiseAction<T>(WorkflowAction workflowAction, DateTime? expiryDate, DateTime? executeOnDate = null) where T : Workflow;
        void RaiseEvent<T>(Enum eventName) where T : Workflow;
        void RaiseEvent<T>(string eventName) where T : Workflow;
        T LoadAction<T>(Guid guid) where T : WorkflowAction;
        WorkflowActionEntity[] LoadActionsForWorkflow(Workflow workflow);
    }
}