﻿using System.Collections.Generic;
using Product.API.Queries;
using Product.DataModels;

namespace Product.API.Services
{
    public interface IProductionMonitoringService
    {
        ZonesData GetZones(string wellName);
        UnitOfMeasure GetUom();
        ZoneFlowData GetZones(ZonesQuery zonesQuery);
        FlowLimitInfo GetLimits();

        /// <summary>
        /// Gets an array of string keys for available data.
        /// </summary>
        /// <param name="wellName">well name</param>
        /// <returns>returns an array of string keys for available data.</returns>
        IEnumerable<string> GetPressure(string wellName);
        RateData GetPressureRates(string wellName, string key);
        IEnumerable<string> GetFlow(string wellName);
        RateData GetFlowRates(string wellName, string key);
    }
}