using System;

namespace Product.API.Queries
{
    public class ZonesQuery
    {
        public string WellName { get; set; }
        public string Periodicity { get; set; }
        public string Period { get; set; }
        public string ZoneNumber { get; set; }
        public bool RecordsCompression { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}