using System;
// ReSharper disable CheckNamespace

namespace Stateflow
{
    public class InvalidActionException : Exception
    {
        public InvalidActionException() : base("The action could not be deserialized")
        {
        }
    }
}