 
using Product.Application.Features.Products.Commands.CreateProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Infrastructure.Repositories
{
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly Dictionary<string, Domain.Entities.Product> Products = new Dictionary<string, Domain.Entities.Product>();

        public Task CreateProduct(Domain.Entities.Product product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduct(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Product> GetProduct(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Domain.Entities.Product>> GetProducts()
        {
            throw new NotImplementedException();
        }
    }
}
