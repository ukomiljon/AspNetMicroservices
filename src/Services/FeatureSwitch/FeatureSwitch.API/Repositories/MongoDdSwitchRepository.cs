
using FeatureSwitch.API.Models;
using FeatureSwitch.API.Shared;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeatureSwitch.API.Repositories
{
    public class MongoDdSwitchRepository : ISwitchRepository
    {
        private IDatabaseSettings _settings;
        private readonly IMongoCollection<Switch> _switches;

        public MongoDdSwitchRepository(IDatabaseSettings settings)
        {
            _settings = settings;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _switches = database.GetCollection<Switch>(settings.CollectionName);
        }

        public async Task<Switch> Get(string email, string featureName)
        {
            return await _switches
                        .Find(_ => _.Email == email && _.FeatureName == featureName)
                        .FirstOrDefaultAsync();
        }

        public async Task<bool> Update(Switch @switch)
        {
            var feature = await this.Get(@switch.Email, @switch.FeatureName);
            @switch.Id = feature.Id;
            var result = await _switches.ReplaceOneAsync(_ => _.Id == feature.Id, @switch);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task Create(Switch @switch)
        {
             await _switches.InsertOneAsync(@switch);
        }
    }
}
