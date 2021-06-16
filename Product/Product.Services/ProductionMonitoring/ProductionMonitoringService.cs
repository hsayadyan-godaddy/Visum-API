using Product.API.Queries;
using Product.DataModels;
using System.Collections.Generic;

namespace Product.Services.ProductionMonitoring
{
    internal class ProductionMonitoringService : IProductionMonitoringService
    {
        public IEnumerable<string> GetFlow(string wellName)
        {
            throw new System.NotImplementedException();
        }

        public RateData GetFlowRates(string wellName, string key)
        {
            throw new System.NotImplementedException();
        }

        public FlowLimitInfo GetLimits()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<string> GetPressure(string wellName)
        {
            throw new System.NotImplementedException();
        }

        public RateData GetPressureRates(string wellName, string key)
        {
            throw new System.NotImplementedException();
        }

        public UnitOfMeasure GetUom()
        {
            throw new System.NotImplementedException();
        }

        public ZonesData GetZones(string wellName)
        {
            throw new System.NotImplementedException();
        }

        public ZoneFlowData GetZones(ZonesQuery zonesQuery)
        {
            throw new System.NotImplementedException();
        }
    }
}
