using Product.DAL.Simulation.Abstraction;
using Product.DataModels;
using Product.DataModels.Basics;
using Product.DataModels.Constants;
using Product.DataModels.Enums;
using System.Collections.Generic;

namespace Product.DAL.Simulation
{
    internal class SimulatedInfoRepository : ISimulatedInfoRepository
    {
        public FlowAcceptableLimits GetFlowAcceptableLimits()
        {
            return new FlowAcceptableLimits
            {
                Gas = new MinMax<double?>(15, 200),
                Oil = new MinMax<double?>(80, 900),
                Water = new MinMax<double?>(1, 50),
            };
        }

        public List<SensorInfo> GetFlowRateInfo()
        {
            return new List<SensorInfo>
            {
                new SensorInfo
                {
                   Id = "Flow-Rate-Surfasce",
                   Name ="Surface flow rate",
                   SourceType = SourceType.FlowRate
                }
            };
        }

        public List<SensorInfo> GetPressureSensorsInfo()
        {
            return new List<SensorInfo>
            {
                new SensorInfo
                {
                   Id = "Bottom-Hole-Pressure-5000",
                   Name ="Bottom Hole Pressure (5000 ft)",
                   SourceType = SourceType.Pressure
                },
                new SensorInfo
                {
                   Id = "Pressure-P1-100",
                   Name ="Pressure Sensor P1 (100 ft)",
                   SourceType = SourceType.Pressure
                },
                new SensorInfo
                {
                   Id = "Pressure-P2-1000",
                   Name ="Pressure Sensor P2 (1000 ft)",
                   SourceType = SourceType.Pressure
                },
                new SensorInfo
                {
                   Id = "Pressure-P3-3000",
                   Name ="Pressure Sensor P3 (3000 ft)",
                   SourceType = SourceType.Pressure
                }
            };
        }

        public UnitOfMeasureInfo GetUnitOfMeasureInfo(SourceType sourceType)
        {
            UnitOfMeasureInfo ret = default;

            switch (sourceType)
            {
                case SourceType.Oil:
                    ret = new UnitOfMeasureInfo
                    {
                        Label = UnitsConstants.Bmp,
                        SourceToMeasure = SourceType.Oil,
                        SystemOfUnits = SystemOfUnits.Customary
                    };
                    break;
                case SourceType.Gas:
                    ret = new UnitOfMeasureInfo
                    {
                        Label = UnitsConstants.CuFt,
                        SourceToMeasure = SourceType.Gas,
                        SystemOfUnits = SystemOfUnits.Customary
                    };
                    break;
                case SourceType.Water:
                    ret = new UnitOfMeasureInfo
                    {
                        Label = UnitsConstants.Bmp,
                        SourceToMeasure = SourceType.Water,
                        SystemOfUnits = SystemOfUnits.Customary
                    };
                    break;
                case SourceType.Depth:
                    ret = new UnitOfMeasureInfo
                    {
                        Label = UnitsConstants.Ft,
                        SourceToMeasure = SourceType.Depth,
                        SystemOfUnits = SystemOfUnits.Customary
                    };
                    break;
                case SourceType.Pressure:
                    ret = new UnitOfMeasureInfo
                    {
                        Label = UnitsConstants.Psi,
                        SourceToMeasure = SourceType.Pressure,
                        SystemOfUnits = SystemOfUnits.Customary
                    };
                    break;
                case SourceType.FlowRate:
                    ret = new UnitOfMeasureInfo
                    {
                        Label = string.Empty,
                        SourceToMeasure = SourceType.FlowRate,
                        SystemOfUnits = SystemOfUnits.Customary
                    };
                    break;
                case SourceType.Unknown:
                default:
                    break;
            }

            return ret;
        }
    }
}
