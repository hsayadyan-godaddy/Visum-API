using Product.PseudoData.Abstraction;
using System;

namespace Product.DAL.Simulation
{
    public class TimeAndDouble : IDataValue
    {
        public double Value { get; set; }
        public DateTime Time { get; set; }
    }
}
