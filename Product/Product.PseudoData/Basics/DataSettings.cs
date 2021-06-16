namespace Product.PseudoData.Basics
{
    public class DataSettings
    {
        public BehaviourType BehaviourType { get; set; } = BehaviourType.Regular;
        public double Mean { get; set; } = 500;
        public double StdDev { get; set; } = 0.25;
    }
}
