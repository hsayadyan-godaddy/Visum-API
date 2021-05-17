using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace VisumData
{
    public class WellData
    {
        public string WellId { get; set; }
        public string Pressure { get; set; }
        public string Temperature { get; set; }
        public string DateTime { get; set; } 
    }
}