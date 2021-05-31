using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeatureSwitch.API.Models
{
    public class Switch
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("featureName")]
        public string FeatureName { get; set; }
        [BsonElement("enable")]
        public bool Enable { get; set; }
    }

}
