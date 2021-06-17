using Product.DAL.Simulation.Abstraction;
using Product.DataModels;
using Product.DataModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Product.DAL.Simulation
{
    internal class SimulatedDataRepository : ISimulatedDataRepository
    {
        #region members

        private readonly DataSimulator _simulator;
        private readonly object _locker = new object();
        private readonly Dictionary<string, Dictionary<string, (string, Func<TimeValue, bool>)>> _callbacksFlowRate
                                = new Dictionary<string, Dictionary<string, (string, Func<TimeValue, bool>)>>();

        private readonly Dictionary<string, Dictionary<string, (string, Func<TimeValue, bool>)>> _callbacksPressure
                                = new Dictionary<string, Dictionary<string, (string, Func<TimeValue, bool>)>>();

        private readonly Dictionary<string, Dictionary<string, (string, string, string, Func<ZoneFlowTimeOilWaterGas, bool>)>> _callbacksOil
                                = new Dictionary<string, Dictionary<string, (string, string, string, Func<ZoneFlowTimeOilWaterGas, bool>)>>();


        #endregion

        #region ctor

        public SimulatedDataRepository()
        {
            _simulator = new DataSimulator();
            _simulator.RealtimeUpdates += OnRealtimeUpdates;
        }

        #endregion

        #region publics

        public void FlowRateDataUpdates(string connectionId, string sensorId, Func<TimeValue, bool> callback)
        {
            var key = GetKey(SourceType.FlowRate, sensorId);

            lock (_locker)
            {
                if (!_callbacksFlowRate.ContainsKey(key))
                {
                    _callbacksFlowRate.Add(key, new Dictionary<string, (string, Func<TimeValue, bool>)>());
                }

                var connections = _callbacksFlowRate[key];
                if (connections.ContainsKey(connectionId))
                {
                    if (callback != null)
                    {
                        connections[connectionId] = (key, callback);
                    }
                    else
                    {
                        connections.Remove(connectionId);
                    }
                }
                else
                {
                    if (callback != null)
                    {
                        connections.Add(connectionId, (key, callback));
                    }
                }
            }
        }

        public FlowRateData GetFlowRateData(string sensorId, Periodicity periodicity, long snapshotSize, DateTime? fromDate, DateTime? toDate)
        {
            var key = GetKey(SourceType.FlowRate, sensorId);
            var range = GetRange(periodicity, fromDate, toDate);

            var tmp = _simulator.GetHistory(key, SourceType.FlowRate, range.Item1, range.Item2, snapshotSize);
            return new FlowRateData
            {
                SensorId = sensorId,
                Data = tmp.Select(x => new TimeValue(x.Time, x.Value)).ToList()
            };
        }

        public PressureData GetPressureData(string sensorId, Periodicity periodicity, long snapshotSize, DateTime? fromDate, DateTime? toDate)
        {
            var key = GetKey(SourceType.Pressure, sensorId);
            var range = GetRange(periodicity, fromDate, toDate);

            var tmp = _simulator.GetHistory(key, SourceType.Pressure, range.Item1, range.Item2, snapshotSize);
            return new PressureData
            {
                SensorId = sensorId,
                Data = tmp.Select(x => new TimeValue(x.Time, x.Value)).ToList()
            };
        }

        public ZoneFlowData GetZoneFlowProductionData(DepthType depthType, int zoneNumber, Periodicity periodicity, long snapshotSize, DateTime? fromDate, DateTime? toDate)
        {
            var range = GetRange(periodicity, fromDate, toDate);

            var key = GetKey(SourceType.Oil, zoneNumber);
            var tmpOil = _simulator.GetHistory(key, SourceType.Oil, range.Item1, range.Item2, snapshotSize);

            key = GetKey(SourceType.Water, zoneNumber);
            var tmpWater = _simulator.GetHistory(key, SourceType.Water, range.Item1, range.Item2, snapshotSize);

            key = GetKey(SourceType.Gas, zoneNumber);
            var tmpGas = _simulator.GetHistory(key, SourceType.Gas, range.Item1, range.Item2, snapshotSize);

            var rdata = new List<ZoneFlowTimeOilWaterGas>();
            for (int i = 0; i < tmpOil.Count; i++)
            {
                rdata.Add(new ZoneFlowTimeOilWaterGas
                {
                    Oil = tmpOil[i].Value,
                    Time = tmpOil[i].Time,
                    Gas = tmpGas.Count > i ? tmpGas[i].Value : 0,
                    Water = tmpWater.Count > i ? tmpWater[i].Value : 0
                });
            }

            return new ZoneFlowData
            {
                ZoneNumber = zoneNumber,
                Data = rdata
            };
        }

        public ZonesData GetZonesData(DepthType depthType)
        {
            var data = new List<ZoneInfo>();
            var num = 1;
            var zoneStep = 750;

            for (int i = 0; i < 15000; i += zoneStep)
            {
                data.Add(new ZoneInfo
                {
                    ZoneNumber = num++,
                    DepthFrom = i,
                    DepthTo = i + zoneStep,
                });
            }

            return new ZonesData
            {
                DepthType = depthType,
                Data = data
            };
        }

        public void PressureDataUpdates(string connectionId, string sensorId, Func<TimeValue, bool> callback)
        {
            var key = GetKey(SourceType.Pressure, sensorId);

            lock (_locker)
            {
                if (!_callbacksPressure.ContainsKey(key))
                {
                    _callbacksPressure.Add(key, new Dictionary<string, (string, Func<TimeValue, bool>)>());
                }

                var connections = _callbacksPressure[key];
                if (connections.ContainsKey(connectionId))
                {
                    if (callback != null)
                    {
                        connections[connectionId] = (key, callback);
                    }
                    else
                    {
                        connections.Remove(connectionId);
                    }
                }
                else
                {
                    if (callback != null)
                    {
                        connections.Add(connectionId, (key, callback));
                    }
                }
            }
        }

        public void ZoneFlowProductionDataUpdates(string connectionId, DepthType depthType, int zoneNumber, Func<ZoneFlowTimeOilWaterGas, bool> callback)
        {
            var key = GetKey(SourceType.Oil);

            var keyOil = GetKey(SourceType.Oil, zoneNumber);
            var keyWater = GetKey(SourceType.Water, zoneNumber);
            var keyGas = GetKey(SourceType.Gas, zoneNumber);

            lock (_locker)
            {
                if (!_callbacksOil.ContainsKey(key))
                {
                    _callbacksOil.Add(key, new Dictionary<string, (string, string, string, Func<ZoneFlowTimeOilWaterGas, bool>)>());
                }

                var connections = _callbacksOil[key];
                if (connections.ContainsKey(connectionId))
                {
                    if (callback != null)
                    {
                        connections[connectionId] = (keyOil, keyWater, keyGas, callback);
                    }
                    else
                    {
                        connections.Remove(connectionId);
                    }
                }
                else
                {
                    if (callback != null)
                    {
                        connections.Add(connectionId, (keyOil, keyWater, keyGas, callback));
                    }
                }
            }
        }

        #endregion

        #region privates

        private void OnRealtimeUpdates(DateTime time)
        {
            List<(string, Func<TimeValue, bool>)> flowRateCalls = null;
            List<(string, Func<TimeValue, bool>)> pressureCalls = null;
            List<(string, string, string, Func<ZoneFlowTimeOilWaterGas, bool>)> oilCalls = null;

            lock (_locker)
            {
                flowRateCalls = _callbacksFlowRate.Values.SelectMany(x => x.Values).ToList();
                pressureCalls = _callbacksPressure.Values.SelectMany(x => x.Values).ToList();
                oilCalls = _callbacksOil.Values.SelectMany(x => x.Values).ToList();
            }

            foreach (var itm in flowRateCalls)
            {
                var tick = _simulator.GetTick(itm.Item1, SourceType.FlowRate);
                itm.Item2.Invoke(new TimeValue(time, tick));
            }

            foreach (var itm in pressureCalls)
            {
                var tick = _simulator.GetTick(itm.Item1, SourceType.Pressure);
                itm.Item2.Invoke(new TimeValue(time, tick));
            }

            foreach (var itm in oilCalls)
            {
                var tickOil = _simulator.GetTick(itm.Item1, SourceType.Oil);
                var tickWater = _simulator.GetTick(itm.Item2, SourceType.Water);
                var tickGas = _simulator.GetTick(itm.Item3, SourceType.Gas);

                itm.Item4.Invoke(new ZoneFlowTimeOilWaterGas
                {
                    Oil = tickOil,
                    Water = tickWater,
                    Gas = tickGas,
                    Time = time
                });
            }
        }

        private (DateTime, DateTime) GetRange(Periodicity periodicity, DateTime? fromDate, DateTime? toDate)
        {
            DateTime dFrom = DateTime.MinValue;
            DateTime dTo = DateTime.Now;

            switch (periodicity)
            {
                case Periodicity.FromRange:
                    {
                        if (fromDate.HasValue)
                        {
                            dFrom = fromDate.Value;
                        }

                        if (toDate.HasValue)
                        {
                            dTo = toDate.Value;
                        }
                    }
                    break;
                case Periodicity.OneYear:
                    {
                        dTo = DateTime.Now;
                        dFrom = DateTime.Now.AddYears(-1);
                    }
                    break;
                case Periodicity.Days90:
                    {
                        dTo = DateTime.Now;
                        dFrom = DateTime.Now.AddDays(-90);
                    }
                    break;
                case Periodicity.Days60:
                    {
                        dTo = DateTime.Now;
                        dFrom = DateTime.Now.AddDays(-60);
                    }
                    break;
                case Periodicity.Days30:
                    {
                        {
                            dTo = DateTime.Now;
                            dFrom = DateTime.Now.AddDays(-30);
                        }
                    }
                    break;
                case Periodicity.Days7:
                    {
                        dTo = DateTime.Now;
                        dFrom = DateTime.Now.AddDays(-7);
                    }
                    break;
                case Periodicity.Hours24:
                    {
                        dTo = DateTime.Now;
                        dFrom = DateTime.Now.AddHours(-24);
                    }
                    break;
                case Periodicity.All:
                default:
                    break;
            }

            if (dTo < dFrom)
            {
                dTo = dFrom.AddMinutes(1);
            }

            if ((dTo - dFrom).TotalDays > 1900)
            {
                dFrom = dTo.AddYears(-5);
            }

            return (dFrom, dTo);

        }

        private string GetKey(SourceType source, params object[] val)
        {
            var valKey = val == null ? string.Empty : string.Join('-', val);
            return $"{source}-{valKey}";
        }

        #endregion 
    }
}
