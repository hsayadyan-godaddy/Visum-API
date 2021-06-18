﻿using Product.PseudoData.Basics;

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
            StdDev = 0.15
        };

        public static readonly DataSettings DataSettingsWater = new DataSettings
        {
            BehaviourType = BehaviourType.Down,
            Mean = 5,
            StdDev = 0.05
        };

        public static readonly DataSettings DataSettingsPressure20Down = new DataSettings
        {
            BehaviourType = BehaviourType.Down,
            Mean = 20,
            StdDev = 0.02
        };

        public static readonly DataSettings DataSettingsPressure50Down = new DataSettings
        {
            BehaviourType = BehaviourType.Down,
            Mean = 50,
            StdDev = 0.03
        };

        public static readonly DataSettings DataSettingsPressure5Up = new DataSettings
        {
            BehaviourType = BehaviourType.Down,
            Mean = 5,
            StdDev = 0.003
        };
    }
}