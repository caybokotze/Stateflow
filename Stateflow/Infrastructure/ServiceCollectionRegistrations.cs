using Microsoft.Extensions.DependencyInjection;

// ReSharper disable CheckNamespace
namespace Stateflow
{
    public static class ServiceCollectionRegistrations
    {
        public static IServiceCollection RegisterStateflow(this IServiceCollection collection)
        {
            collection.AddSingleton<Workflow>();
            return collection;
        }
    }
}