// ReSharper disable CheckNamespace
namespace Stateflow
{
    public class EventConfigured
    {
        public IWorkflowConfiguration Configuration { get; }

        public EventConfigured(IWorkflowConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}