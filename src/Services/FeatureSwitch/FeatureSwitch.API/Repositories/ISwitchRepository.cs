using FeatureSwitch.API.Models; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeatureSwitch.API.Repositories
{
    public interface ISwitchRepository
    {
        Task<Switch> Get(string email, string featureName);
        Task<bool> Update(Switch @switch);
        Task Create(Switch @switch);
    }
}
