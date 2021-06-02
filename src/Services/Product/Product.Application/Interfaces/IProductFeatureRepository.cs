using EventBus.Messages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Application.Features.Products.Commands.CreateProduct
{
    public interface IProductFeatureRepository
    {
        Task<SwitchFeatureEvent> Get(string email, string featureName);
        Task<SwitchFeatureEvent> Get(SwitchFeatureEvent @switch);
        Task<bool> IsEnabled(string email, string featureName);
        Task<bool> Update(SwitchFeatureEvent @switch);
        Task Create(SwitchFeatureEvent @switch);
    }
}