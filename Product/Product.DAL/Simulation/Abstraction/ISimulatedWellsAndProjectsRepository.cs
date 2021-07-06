using Product.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.DAL.Simulation.Abstraction
{
    public interface ISimulatedWellsAndProjectsRepository
    {
        Task<PagedResult<WellboreInfo>> GetWellsAsync(string searchFilter, int resultsPerPage, int pageIndex, string projectId);
        Task<List<string>> GetWellNamesToCompleteAsync(string searchFilter, string projectId, int maxResults);
    }
}
