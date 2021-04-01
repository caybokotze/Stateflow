// ReSharper disable CheckNamespace
namespace Stateflow
{
    public class StateConfigured
    {
        public IWorkflowConfiguration Configuration { get; }

        public StateConfigured(IWorkflowConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}