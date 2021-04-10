using System;
using System.Data;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable CheckNamespace
namespace Stateflow
{
    public interface IWorkflowService
    {
        IDbConnection DbConnection { get; }
        DatabaseProvider DatabaseProvider { get; }
        IServiceProvider ServiceProvider { get; }
        string Schema { get; }
    }
}