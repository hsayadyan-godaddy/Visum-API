using Product.DataModels.Enums;
using Product.PseudoData;
using System;
using System.Collections.Generic;
using System.Timers;

namespace Product.DAL.Simulation
{
    public class DataSimulator
    {
        #region members

        private readonly Dictionary<string, DataGenerator> _generators = new Dictionary<string, DataGenerator>();
        private readonly DataCountOptimizer _optimizer = new DataCountOptimizer();
        private readonly object _locker = new object();

        #endregion // members

        #region events

        public delegate void RealtimeUpdate(DateTime time);
        public event RealtimeUpdate RealtimeUpdates;

        #endregion // events

        public DataSimulator()
        {
            CreateRealtime();
        }

        public List<TimeAndDouble> GetHistory(string key, SourceType sourceType, DateTime fromTime, DateTime toTime, long adjustCount, bool includeTimeValue = true)
        {
            var gen = GetGenerator(key, sourceType, true);
            var count = (int)(toTime - fromTime).TotalMinutes + 1;

            var data = gen.Next(count);

            var time = fromTime;
            var tmp = new List<TimeAndDouble>();

            foreach (var itm in data)
            {

                tmp.Add(new TimeAndDouble
                {
                    Time = time,
                    Value = itm
                });

                if (includeTimeValue)
                {
                    time = time.AddMinutes(1);
                }
            }

            var ret = _optimizer.OptimizeCount(tmp, adjustCount);

            return ret;
        }

        public double GetTick(string key, SourceType sourceType)
        {
            var gen = GetGenerator(key, sourceType);
            return gen.Next();
        }

        #region privates

        private DataGenerator GetGenerator(string key, SourceType sourceType, bool reset = false)
        {
            DataGenerator ret = null;

            lock (_locker)
            {
                if (!_generators.ContainsKey(key))
                {
                    var setting = PseudoData.Helpers.Common.DataSettingsOil;
                    switch (sourceType)
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
                        case SourceType.FlowRate:
                            setting = PseudoData.Helpers.Common.DataSettingsPressure50Down;
                            break;
                        case SourceType.Pressure:
                            setting = PseudoData.Helpers.Common.DataSettingsPressure5Up;
                            break;
                        case SourceType.AnyRate:
                            setting = PseudoData.Helpers.Common.DataSettingsAnyRate;
                            break;
                        default:
                            break;
                    }
                    var generator = new DataGenerator(setting);
                    _generators.Add(key, generator);
                }

                if (reset)
                {
                    _generators[key].Reset();
                }

                ret = _generators[key];
            }

            return ret;
        }


        private void CreateRealtime()
        {
            var tmr = new Timer();

            tmr.Elapsed += (s, e) =>
            {
                RealtimeUpdates?.Invoke(DateTime.Now);
            };
            tmr.Interval = 200;
            tmr.Start();
        }

        #endregion // privates
    }
}
