using Microsoft.Extensions.DependencyInjection;
using System;

namespace Product.DAL
{
    public static class BootstrapperExtension
    {

        public static void RegisterDataAccessLayerServices(this IServiceCollection service)
        {
           // service.AddScoped<IProductionMonitoringService, ProductionMonitoringService>();
        }

    }
}
