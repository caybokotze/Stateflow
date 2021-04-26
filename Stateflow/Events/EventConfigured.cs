// ReSharper disable CheckNamespace
namespace Stateflow
{
    public class EventConfigured
    {
        public StateConfiguration StateConfiguration { get; }

        public EventConfigured(StateConfiguration stateConfiguration)
        {
            StateConfiguration = stateConfiguration;
        }
    }
}