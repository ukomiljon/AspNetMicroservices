 
using MongoDB.Driver;
using Product.Application.Features.Products.Commands.CreateProduct;
using Product.Application.Features.Products.Commands.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Product.Infrastructure.Repositories
{
    public class MongoDdProductRepository : IProductRepository
    {
        private IDatabaseSettings _settings;
        private readonly IMongoCollection<Domain.Entities.Product> _collection;

        public MongoDdProductRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _collection = database.GetCollection<Domain.Entities.Product>(settings.CollectionName);
            _settings = settings;
        }

        public async Task CreateProduct(Domain.Entities.Product product)
        {
            await _collection.InsertOneAsync(product);
        }

        public async Task DeleteProduct(string id)
        {
            await _collection.DeleteOneAsync(_ => _.Id == id);
        }

        public async Task<Domain.Entities.Product> GetProduct(string id)
        {
            return await _collection
                      .Find(_ => _.Id == id)
                      .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Domain.Entities.Product>> GetProducts()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }
    }
}
