using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VisumAPI.Models
{
    public class Well
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
 
        [BsonElement("WellName")]
        public string WellName { get; set; }
 
        public string WellType { get; set; }

        public string ProjectId { get; set; }
    }

    public class WellData
    {
        public string WellId { get; set; }
        public int Pressure { get; set; }
        public int Temperature { get; set; }
        public BsonDateTime DateTime { get; set; }

    }
}
