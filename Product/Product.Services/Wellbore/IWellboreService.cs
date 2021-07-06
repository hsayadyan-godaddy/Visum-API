using Product.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Services.Wellbore
{
    public interface IWellboreService
    {
        Task<PagedResult<WellboreInfo>> GetWellsAsync(string searchFilter, int resultsPerPage, int pageIndex, string projectId, bool nearbyWells);
        Task<PagedResult<WellboreInfo>> GetRecentWellsAsync(string searchFilter, int resultsPerPage, int pageIndex, string projectId, bool nearbyWells);
        Task<List<string>> GetWellNamesToCompleteAsync(string searchFilter, string projectId, int maxResults);
    }
}
