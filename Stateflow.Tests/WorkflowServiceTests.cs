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
            Expect(workflowService?.DatabaseProvider).To.Equal(DatabaseProvider.MySql);
        }
    }
}