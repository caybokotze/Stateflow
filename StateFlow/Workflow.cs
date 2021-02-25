using System;
using System.ComponentModel;

namespace StateFlow
{
    public abstract class Workflow : StateManagement
    {
        public abstract string Register();

        public virtual void RaiseEvent()
        {
            return;
        }
    }
}