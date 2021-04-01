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
            
            collection.AddTransient<WorkflowService>(ws => 
                new WorkflowService(
                    databaseConnection,
                    collection,
                    serviceProvider,
                    databaseProvider));

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
        // public static List<Workflow> workflows = typeof(Workflow).Assembly.GetTypes()
        //     .Where(w => w.IsSubclassOf(typeof(Workflow)) && !w.IsAbstract).Select(s => s.);
        public static IEnumerable<T> GetEnumerableOfType<T>()
            where T : class, IComparable<T>
        {
            var types = new List<T>();
            
            // ReSharper disable once PossibleNullReferenceException
            foreach (Type type in Assembly
                .GetAssembly(typeof(T))
                ?.GetTypes()
                .Where(myType =>
                myType.IsClass 
                && !myType.IsAbstract 
                && myType.IsSubclassOf(typeof(T))))
            {
                types.Add(type as T);
            }

            return types;
        }
    }
}