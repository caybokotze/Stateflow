using System;
using System.Data;
using System.Data.Common;
using Microsoft.Extensions.DependencyInjection;

namespace StateFlow
{
    public interface IWorkflowService
    {
        IDbConnection DbConnection { get; }
        IServiceCollection ServiceCollection { get; }
        IServiceProvider ServiceProvider { get; }
    }
}