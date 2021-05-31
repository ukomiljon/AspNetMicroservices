
using FeatureSwitch.API.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace FeatureSwitch.API.Shared
{
    public class SwitchEqualityComparer : IEqualityComparer<Switch>
    {
        public bool Equals(Switch x, Switch y)
        {
            return x.Email == y.Email && x.FeatureName == y.FeatureName;
        }

        public int GetHashCode([DisallowNull] Switch obj)
        {
            return obj.Email.GetHashCode() + 13 * obj.FeatureName.GetHashCode();
        }
    }
}
