using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Application.Features.Products.Commands.CreateProduct
{
    public interface IProductRepository
    {
        Task<IEnumerable<Domain.Entities.Product>> GetProducts();
        Task<Domain.Entities.Product> GetProduct(string id);
        
        Task CreateProduct(Domain.Entities.Product product); 
        Task<bool> DeleteProduct(string id);
    }
}