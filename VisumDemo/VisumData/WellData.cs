using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Product.DataModels
{
    public class WellData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string WellId { get; set; }
        public string Pressure { get; set; }
        public string Temperature { get; set; }
        public DateTime DateTime { get; set; } 
    }
}