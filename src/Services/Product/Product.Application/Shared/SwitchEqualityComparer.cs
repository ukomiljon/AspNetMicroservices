

using EventBus.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Application
{
    public class SwitchEqualityComparer : IEqualityComparer<SwitchFeatureEvent>
    {
        public bool Equals(SwitchFeatureEvent x, SwitchFeatureEvent y)
        {
            return x.Email == y.Email && x.FeatureName == y.FeatureName;
        }

        public int GetHashCode([DisallowNull] SwitchFeatureEvent obj)
        {
            return obj.Email.GetHashCode() + 13 * obj.FeatureName.GetHashCode();
        }
    }
}
