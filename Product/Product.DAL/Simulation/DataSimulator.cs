using Product.DataModels.Enums;
using Product.PseudoData;
using Product.PseudoData.Abstraction;
using System;
using System.Collections.Generic;
using System.Timers;

namespace Product.API.Services.Simulation
{
    public class DataSimulator
    {
        #region members

        private readonly Dictionary<string, DataGenerator> _generators = new Dictionary<string, DataGenerator>();

        #endregion // members

        #region events

        public delegate void RealtimeUpdate(DataItemInfo info, double value);
        public event RealtimeUpdate RealtimeUpdates;

        #endregion // events


        public List<IDataValue> GetHistory(DataItemInfo info, DateTime from, DateTime to, int adjustCount)
        {
            var gen = GetGenerator(info, true);
            var count = (int)(to - from).TotalSeconds;
            var data = gen.Next(count);

            return null;
        }

        #region privates

        private DataGenerator GetGenerator(DataItemInfo info, bool reset = false)
        {
            var key = info.ToString();

            if (!_generators.ContainsKey(key))
            {
                var setting = PseudoData.Helpers.Common.DataSettingsOil;
                switch (info.SourceType)
                {
                    case SourceType.Oil:
                        setting = PseudoData.Helpers.Common.DataSettingsOil;
                        break;
                    case SourceType.Gas:
                        setting = PseudoData.Helpers.Common.DataSettingsGas;
                        break;
                    case SourceType.Water:
                        setting = PseudoData.Helpers.Common.DataSettingsWater;
                        break;
                    case SourceType.Pressure:
                        setting = PseudoData.Helpers.Common.DataSettingsPressure50Down;
                        break;
                    default:
                        break;
                }
                var generator = new DataGenerator(setting);
                _generators.Add(key, generator);

                CreateRealtime(generator, info);
            }

            if (reset)
            {
                _generators[key].Reset();
            }

            return _generators[key];
        }


        private void CreateRealtime(DataGenerator generator, DataItemInfo info)
        {
            var tmr = new Timer();
            var genRef = generator;
            var infoRef = info;
         

            tmr.Elapsed += (s, e) =>
            {
                var val = genRef.Next();
                RealtimeUpdates?.Invoke(infoRef, val);
            };
        }

        #endregion // privates
    }
}
