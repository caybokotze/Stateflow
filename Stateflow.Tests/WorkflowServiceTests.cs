using System.Data;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using NExpect;
using NUnit.Framework;
using Stateflow.Tests.Helpers;
using static NExpect.Expectations;

namespace Stateflow.Tests
{
    [TestFixture]
    public class WorkflowServiceTests : TestBase
    {
        [Test]
        public void WorkflowServiceDoesResolve()
        {
            // Arrange
            var workflowService = WorkflowService;
            
            // Act
            
            // Assert
            Expect(workflowService).To.Not.Be.Null();
        }

        [Test]
        public void WorkflowServiceDoesContainValidConnectionString()
        {
            // Arrange
            var workflowService = WorkflowService;
            
            // Act
            
            // Assert
            Expect(workflowService.DbConnection).To.Not.Be.Null();
            Expect(workflowService.DbConnection.ConnectionString).To.Not.Equal(string.Empty);
        }

        [Test]
        public void WhenBuildingWorkflowServiceFactory_DatabaseProviderShouldResolveToCorrectProvider()
        {
            // Arrange
            var sqlConnection = new MySqlConnection("server=localhost;port=3306;database=workflow_test;user=sqltracking;password=sqltracking;");
            var databaseProvider = DatabaseProvider.MySql;
            
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IDbConnection>(sqlConnection)
                .AddSingleton<IWorkflowService>(new WorkflowService(
                        sqlConnection, 
                        null)
                {
                    DatabaseProvider = databaseProvider
                })
                .BuildServiceProvider();
            
            // Act
            var workflowService = serviceProvider
                .GetService<IWorkflowService>();
            
            // Assert
            Expect(workflowService?.DatabaseProvider)
                .To
                .Equal(DatabaseProvider.MySql);
        }

        [TestFixture]
        public class Initialization
        {
            [TestFixture]
            public class InitialiseWorkflows
            {
                [Test]
                public void DoesCreateTablesIfNotExist()
                {
                    // Arrange
                    // Act
                    // Assert
                }

                [Test]
                public void DoesPersistNewWorkflows()
                {
                    // Arrange
                    // Act
                    // Assert
                }

                [Test]
                public void ShouldFindTypeWorkflowInApplicationDomain()
                {
                    // Arrange
                    // Act
                    // Assert
                }

                [Test]
                public void ShouldInjectAWorkflowWithAWorkflowServiceInstance()
                {
                    // Arrange
                    // Act
                    // Assert
                }

                [Test]
                public void ShouldFailToInjectAWorkflowWithMultipleConstructorParameters()
                {
                    // Arrange
                    // Act
                    // Assert
                }

                [Test]
                public void DoesCallTheDefineWorkflowRulesMethodInWorkflowInstance()
                {
                    // Arrange
                    // Act
                    // Assert
                }
            }

            [TestFixture]
            public class InitialiseWorkflow
            {
                [Test]
                public void DoesPersistWorkflowEntity()
                {
                    // Arrange
                    // Act
                    // Assert
                }

                [Test]
                public void DoesThrowNullReferenceIfWorkflowIsNull()
                {
                    // Arrange
                    // Act
                    // Assert
                }
            }

            [TestFixture]
            public class DisposeWorkflow
            {
                [Test]
                public void DoesRemoveAllWorkflowsAndWorkflowStatesForSpecifiedWorkflow()
                {
                    // Arrange
                    // Act
                    // Assert
                }
            }

            [TestFixture]
            public class InitialiseAction
            {
                [Test]
                public void ShouldReturnNewActionInitialisingInstance()
                {
                    // Arrange
                    // Act
                    // Assert
                }

                [Test]
                public void ResultDoesContainNonNullActionEntity()
                {
                    // Arrange
                    // Act
                    // Assert
                }

                [Test]
                public void DoesReturnNonNullWorkflowServiceInstance()
                {
                    // Arrange
                    // Act
                    // Assert
                }


                [Test]
                public void OnWorkflowEventShouldBeCallableOnInitialisedAction()
                {
                    // Arrange
                    // Act
                    // Assert
                }

                [Test]
                public void OnWorkflowStateShouldBeCallableOn_OnWorkflowEvent()
                {
                    // Arrange
                    // Act
                    // Assert
                }

                [Test]
                public void OnWorkflowStateShouldPersistAction()
                {
                    // Arrange
                    // Act
                    // Assert
                }

                [Test]
                public void OnWorkflowEventShouldAssignEventNameToWorkflowAction()
                {
                    // Arrange
                    // Act
                    // Assert
                }
            }

            [TestFixture]
            public class LoadAction
            {
                [Test]
                public void LoadActionShouldDeserializeAWorkflowActionEntityAndReturnAnInstanceOfThatAction()
                {
                    // Arrange
                    // Act
                    // Assert
                }
            }

            [TestFixture]
            public class LoadActiveActionsForWorkflow
            {
                [Test]
                public void ReturnsListOfActionsForWorkflow()
                {
                    // Arrange
                    // Act
                    // Assert
                }
            }
        }
    }
}