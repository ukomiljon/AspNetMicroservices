using System;

namespace EventBus.Messages
{
    public class SwitchFeatureEvent
    {        
        public string Email { get; set; }         
        public string FeatureName { get; set; }       
        public bool Enable { get; set; }
    }
}
