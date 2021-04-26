// ReSharper disable CheckNamespace
namespace Stateflow
{
    public class StateConfigured
    {
        public StateConfiguration StateConfiguration { get; }

        public StateConfigured(
            StateConfiguration stateConfiguration)
        {
            StateConfiguration = stateConfiguration;
        }
    }
}