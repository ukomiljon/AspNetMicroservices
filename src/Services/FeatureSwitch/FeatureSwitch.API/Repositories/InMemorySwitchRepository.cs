
using FeatureSwitch.API.Models;
using FeatureSwitch.API.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeatureSwitch.API.Repositories
{
    public class InMemorySwitchRepository : ISwitchRepository
    {
        private readonly Dictionary<Switch, bool> Switches = new Dictionary<Switch, bool>(new SwitchEqualityComparer());

        public Task Create(Switch @switch)
        {
          
            Switches.TryAdd(@switch, @switch.Enable);

            return Task.Run(() => @switch.Enable);
        }

        public Task<Switch> Get(string email, string featureName)
        {
            var feature = new Switch { Email = email, FeatureName = featureName };

            if (Switches.TryGetValue(feature, out var enabled))
            {
                feature.Enable = enabled;
                return Task.Run(() => feature);
            }

            feature = null;
            return Task.Run(() => feature); 
        }

        public Task<bool> Update(Switch @switch)
        {
            if (!Switches.ContainsKey(@switch))
            {
                return Task.Run(() => false);
            }

            Switches[@switch] = @switch.Enable;
            return Task.Run(() => false);
        }

       
    }
}
