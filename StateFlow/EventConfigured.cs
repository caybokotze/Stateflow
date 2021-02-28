namespace StateFlow
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