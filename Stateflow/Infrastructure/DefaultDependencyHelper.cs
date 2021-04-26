// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Stateflow
{
    public static class DefaultDependencyHelper
    {
        public static IServiceCollection Configure(
            this IServiceCollection collection,
            IDbConnection databaseConnection,
            IServiceProvider serviceProvider,
            DatabaseProvider databaseProvider)
        {
            collection.AddTransient<IWorkflowService, WorkflowService>();
            
            // collection.AddTransient<WorkflowService>(ws => 
            //     new WorkflowService(
            //         databaseConnection,
            //         collection,
            //         serviceProvider,
            //         databaseProvider));

            // Note: I'm not even sure if this is possible or not.
            // var allWorkflows = ReflectiveEnumerator.GetEnumerableOfType<Workflow>();
            
            // allWorkflows
            //     .ToList()
            //     .ForEach(f =>
            //     {
            //         var type = typeof(f);
            //         collection.AddTransient<f>();
            //     });
            
            return collection;
        }
    }

    public static class ReflectiveEnumerator
    {
        public static IEnumerable<Type> GetEnumerableOfEntryType<T>()
            where T : class, IComparable<T>
        {
            var types = new List<Type>();
            
            // ReSharper disable once PossibleNullReferenceException

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            
            foreach(var assembly in assemblies)
            {
                foreach (Type type in 
                    assembly
                    .GetTypes()
                    .Where(myType =>
                    myType.IsClass 
                    && !myType.IsAbstract 
                    && myType.IsSubclassOf(typeof(T))))
                {
                    types.Add(type);
                }
            }

            return types;
        }
    }
}