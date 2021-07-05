using Product.DAL.Simulation.Abstraction;
using Product.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Services.Wellbore
{
    internal class WellboreService : IWellboreService
    {
        #region members

        private readonly ISimulatedWellsAndProjectsRepository _simulatedWellsAndProjects;
        
        #endregion

        #region ctor

        public WellboreService(ISimulatedWellsAndProjectsRepository simulatedWellsAndProjects)
        {
            _simulatedWellsAndProjects = simulatedWellsAndProjects;
        }

        #endregion

        #region publics

        public Task<PagedResult<WellboreInfo>> GetRecentWellsAsync(string searchFilter, int resultsPerPage, int pageIndex, string projectId, bool nearbyWells)
        {
            return _simulatedWellsAndProjects
                        .GetWellsAsync(searchFilter, 
                                       resultsPerPage, 
                                       pageIndex, 
                                       projectId);
        }

        public Task<PagedResult<WellboreInfo>> GetWellsAsync(string searchFilter, int resultsPerPage, int pageIndex, string projectId, bool nearbyWells)
        {
            return _simulatedWellsAndProjects
                         .GetWellsAsync(searchFilter,
                                        resultsPerPage,
                                        pageIndex,
                                        projectId);
        }

        public Task<List<string>> GetWellNamesToCompleteAsync(string searchFilter, string projectId, int maxResults)
        {
            return _simulatedWellsAndProjects
                         .GetWellNamesToCompleteAsync(searchFilter, projectId, maxResults);
        }

        #endregion
    }
}
