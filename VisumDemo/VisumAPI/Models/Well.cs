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

    public class WellInfo : Well
    {
        public string ProjectName { get; set; }
        public string Status { get; set; }
    }

}
