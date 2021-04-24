using Microsoft.Extensions.DependencyInjection;

namespace Stateflow.Tests.Helpers
{
    public class TestBase
    {
        public IWorkflowService WorkflowService { get; }

        public TestBase()
        {
            var serviceProvider = ServiceProviderHelper.BuildServiceProvider();
            WorkflowService = serviceProvider.GetService<IWorkflowService>();
        }
    }
}