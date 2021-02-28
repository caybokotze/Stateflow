using Microsoft.Extensions.DependencyInjection;

namespace StateFlow
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