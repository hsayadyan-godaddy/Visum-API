using Microsoft.Extensions.DependencyInjection;
using Product.DAL.Simulation;
using Product.DAL.Simulation.Abstraction;

namespace Product.DAL
{
    public static class BootstrapperExtension
    {

        public static void RegisterDataAccessLayerServices(this IServiceCollection service)
        {
            service.AddSingleton<ISimulatedDataRepository, SimulatedDataRepository>();
            service.AddSingleton<ISimulatedInfoRepository, SimulatedInfoRepository>();
            service.AddSingleton<ISimulatedWellsAndProjectsRepository, SimulatedWellsAndProjectsRepository>();
            
        }

    }
}
