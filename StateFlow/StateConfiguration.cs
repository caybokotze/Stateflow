using System.Security.Principal;

namespace StateFlow
{
    public class StateConfiguration
    {
        public StateConfiguration()
        {
            Complete = false;
        }
        
        public bool Initialised { get; set; }
        public string StateName { get; set; }
        public bool Complete { get; set; }
    }
}