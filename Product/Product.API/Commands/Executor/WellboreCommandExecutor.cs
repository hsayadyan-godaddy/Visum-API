using Microsoft.AspNetCore.Http;
using Product.API.Commands.Abstraction;
using Product.API.Commands.CommandModel.Wellbore;
using Product.API.Models.Wellbore;
using Product.Services.Wellbore;
using System.Threading.Tasks;

namespace Product.API.Commands.Executor
{
    /// <summary>
    /// Production Monitoring CommandExecutor
    /// </summary>
    public class WellboreCommandExecutor : IAsyncCommandExecutor<WellboreSearchCommand, WellboreSearchResponse>,
                                           IAsyncCommandExecutor<WellboreNamesToCompleteCommand, WellboreNamesToCompleteResponse>

    {
        #region members

        private readonly IWellboreService _wellboreService;

        #endregion

        #region ctor

        /// <summary>
        /// Create new instance
        /// </summary>
        /// <param name="wellboreService"></param>
        public WellboreCommandExecutor(IWellboreService wellboreService)
        {
            _wellboreService = wellboreService;
        }

        #endregion

        #region publics

        /// <summary>
        /// Search wells
        /// </summary>
        /// <param name="command"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<WellboreSearchResponse> ExecuteAsync(WellboreSearchCommand command, HttpContext context)
        {
            var result = command.RecentWells ? await _wellboreService.GetWellsAsync(command.SearchString,
                                                                                    command.ResultsPerPage,
                                                                                    command.PageIndex,
                                                                                    command.CurrentProjectId,
                                                                                    command.NearbyWellsOnly)

                                             : await _wellboreService.GetRecentWellsAsync(command.SearchString,
                                                                                          command.ResultsPerPage,
                                                                                          command.PageIndex,
                                                                                          command.CurrentProjectId,
                                                                                          command.NearbyWellsOnly);

            var ret = new WellboreSearchResponse
            {
                CurrentPageIndex = result.CurrentPageIndex,
                TotalPages = result.TotalPages,
                Result = result.Result
            };

            return ret;
        }

        /// <summary>
        /// Search wells names for autocomplete
        /// </summary>
        /// <param name="command"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<WellboreNamesToCompleteResponse> ExecuteAsync(WellboreNamesToCompleteCommand command, HttpContext context)
        {
            var result = await _wellboreService.GetWellNamesToCompleteAsync(command.SearchString,
                                                                                    command.CurrentProjectId,
                                                                                    command.MaxResults);

            var ret = new WellboreNamesToCompleteResponse
            {
                Result = result
            };

            return ret;
        }

        #endregion
    }

}
