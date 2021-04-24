﻿using System;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using Newtonsoft.Json;
using Stateflow.Entities;

// ReSharper disable CheckNamespace
namespace Stateflow
{
    public class WorkflowService : IWorkflowService
    {
        public WorkflowService(
            IDbConnection dbConnection,
            IServiceProvider serviceProvider,
            DatabaseProvider databaseProvider,
            bool splitActionTables)
        {
            DbConnection = dbConnection;
            ServiceProvider = serviceProvider;
            DatabaseProvider = databaseProvider;
            SplitActionTables = splitActionTables;
        }
        
        public IDbConnection DbConnection { get; }
        public DatabaseProvider DatabaseProvider { get; }
        public string Schema { get; }
        public bool SplitActionTables { get; }
        public IServiceProvider ServiceProvider { get; }

        private void CreateTablesIfNotExist()
        {
            StateflowDbContext.DDL.CreateWorkflowTable(this);
            StateflowDbContext.DDL.CreateWorkflowActionsTable(this);
            StateflowDbContext.DDL.CreateWorkflowStatesTable(this);
        }
        
        public void InitialiseWorkflows()
        {
            CreateTablesIfNotExist();
            
            var workflows =  ReflectiveEnumerator
                .GetEnumerableOfEntryType<Workflow>()
                .ToList();

            if (!workflows.Any()) throw new NoWorkflowsFoundException();

            foreach (var workflow in workflows)
            {
                object typeInstance;
                try
                {
                    typeInstance = Activator.CreateInstance(workflow, this);
                }
                catch
                {
                    throw new ConstructorParametersNotDefined();
                }

                var workflowInstance = (Workflow) typeInstance;
                
                InitialiseWorkflow(workflowInstance?.GetType().Name);

                if (typeInstance is null)
                {
                    throw new InvalidOperationException("Workflow Service tried to initialise an invalid workflow.");
                }

                workflowInstance.DefineWorkflowRules();
            }
        }

        private void InitialiseWorkflow(string workflowName)
        {
            if (workflowName is null)
            {
                throw new NullReferenceException("The workflow specified is null");
            }
            
            var workflow = new WorkflowEntity
            {
                CurrentState = StateManager.GlobalState.Initialise,
                Uuid = Guid.NewGuid(),
                IsActive = true,
                WorkflowName = workflowName,
                DateCreated = DateTime.UtcNow
            };

            StateflowDbContext
                .Commands
                .CreateOrUpdateWorkflow(this, workflow);
        }

        public void DisposeWorkflow<T>() where T : Workflow
        {
            var workflowType = typeof(T);
            var workflowName = workflowType.Name;

            var workflow = StateflowDbContext
                .Queries
                .FetchWorkflowByName(this, workflowName);
            
            // note: States and Workflows are fine to delete, since they will get generated when the app runs.
            // The same can not be said for actions. Those should be persisted.

            DbConnection.Query("START TRANSACTION;");
            
            StateflowDbContext
                .Commands
                .DeleteWorkflowByUuid(this, workflow.Uuid);

            StateflowDbContext
                .Commands
                .DeleteWorkflowStatesByWorkflowUuid(this, workflow.Uuid);

            DbConnection.Query("COMMIT;");
        }

        public ActionInitialising InitialiseAction<T>(
            WorkflowAction workflowAction, 
            DateTime expiryDate, 
            DateTime? executeOnDate = null) where T : Workflow
        {
            var workflowType = typeof(T);
            var workflowName = workflowType.Name;

            var actionType = workflowAction.GetType();
            var actionName = actionType.Name;

            var workflow = StateflowDbContext
                .Queries
                .FetchWorkflowByName(this, workflowName);
            
            var actionData = workflowAction.GetData();

            var output = JsonConvert.SerializeObject(actionData);

            var actionEntity = new WorkflowActionEntity
            {
                Uuid = Guid.NewGuid(),
                WorkflowUuid = workflow.Uuid,
                Retries = 0,
                ActionBody = output,
                ActionName = actionName,
                ActionEvent = string.Empty,
                IsComplete = false,
                DateToExecute = executeOnDate,
                DateExpires = null,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
                DateProcessed = DateTime.UtcNow
            };

            return new ActionInitialising(actionEntity, this);
        }

        public void RaiseEvent<T>(Enum eventName) where T : Workflow
        {
            RaiseEvent<T>(eventName.ToString());
        }


        public void RaiseEvent<T>(string eventName) where T : Workflow
        {
            
        }
    }
}