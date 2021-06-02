using EventBus.Messages;
using Product.Application;
using Product.Application.Features.Products.Commands.CreateProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Repositories
{
    class InMemoryProductFeatureRepository : IProductFeatureRepository
    {
        private readonly Dictionary<SwitchFeatureEvent, bool> FeaturesStatus = new Dictionary<SwitchFeatureEvent, bool>(new SwitchEqualityComparer());

        public   Task Create(SwitchFeatureEvent @switch)
        {
            FeaturesStatus.TryAdd(@switch, @switch.Enable);

            return Task.Run(() => @switch.Enable);
        }

        public   Task<SwitchFeatureEvent> Get(string email, string featureName)
        {
            var feature = new SwitchFeatureEvent { Email = email, FeatureName = featureName };

            if (FeaturesStatus.TryGetValue(feature, out var enabled))
            {
                feature.Enable = enabled;
                return Task.Run(() => feature);
            }

            feature = null;
            return Task.Run(() => feature);
        }

        public   Task<bool> Update(SwitchFeatureEvent @switch)
        {
            if (!FeaturesStatus.ContainsKey(@switch))
            {
                return Task.Run(() => false);
            }

            FeaturesStatus[@switch] = @switch.Enable;
            return Task.Run(() => false);
        }
    }
}
