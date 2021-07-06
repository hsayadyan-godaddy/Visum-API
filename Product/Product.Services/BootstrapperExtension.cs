using Microsoft.Extensions.DependencyInjection;
using Product.DAL;
using Product.Services.ProductionMonitoring;
using Product.Services.Wellbore;

namespace Product.Services
{
    public static class BootstrapperExtension
    {
        public static void RegisterServicesFactory(this IServiceCollection service)
        {
            service.RegisterDataAccessLayerServices();

            service.AddScoped<IProductionMonitoringService, ProductionMonitoringService>();
            service.AddScoped<IWellboreService, WellboreService>();
        }
    }
}
