using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace VisumAPI.Models
{
    public class Project
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        //position: number,

        //projectName: string, 
        [BsonElement("Name")]
        public string ProjectName { get; set; }

        //country: string, 
        public string Country { get; set; }

        //reservoir: string, 
        public string Reservoir { get; set; }

        //pad:string,
        public string Pad { get; set; }

        //api:string, 
        public string API { get; set; }

        //field:string, 
        public string Field { get; set; }

        //customer:string
        public string  UserId { get; set; }
    }

    public class ProjectList
    {
        public List<Project> Projects { get; set; }

        public Customer Customer { get; set; }
    }
}
