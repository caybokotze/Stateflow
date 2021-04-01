using System;

// ReSharper disable CheckNamespace
namespace Stateflow
{
    public abstract class WorkflowEntity
    {
        public int Id { get; set; }
        public Guid ReferenceId { get; set; }
        public string Data { get; set; }
        public int RetryCount { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateProcessed { get; set; }
    }
}