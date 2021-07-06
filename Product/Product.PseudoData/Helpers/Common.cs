using Product.PseudoData.Basics;

namespace Product.PseudoData.Helpers
{
    public class Common
    {
        public static readonly DataSettings DataSettingsOil = new DataSettings
        {
            BehaviourType = BehaviourType.Down,
            Mean = 500,
            StdDev = 0.25
        };

        public static readonly DataSettings DataSettingsGas = new DataSettings
        {
            BehaviourType = BehaviourType.Down,
            Mean = 200,
            StdDev = 0.015
        };

        public static readonly DataSettings DataSettingsWater = new DataSettings
        {
            BehaviourType = BehaviourType.Down,
            Mean = 100,
            StdDev = 0.005
        };

        public static readonly DataSettings DataSettingsPressure20Down = new DataSettings
        {
            BehaviourType = BehaviourType.Down,
            Mean = 20,
            StdDev = 0.002
        };

        public static readonly DataSettings DataSettingsPressure50Down = new DataSettings
        {
            BehaviourType = BehaviourType.Down,
            Mean = 50,
            StdDev = 0.003
        };

        public static readonly DataSettings DataSettingsPressure5Up = new DataSettings
        {
            BehaviourType = BehaviourType.Down,
            Mean = 5,
            StdDev = 0.0003
        };

        public static readonly DataSettings DataSettingsAnyRate = new DataSettings
        {
            BehaviourType = BehaviourType.Regular,
            Mean = 0.12,
            StdDev = 0.04
        };
    }
}
