using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Product.API.Commands.Abstraction
{
    /// <summary>
    /// Command Executor Generic
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public interface IAsyncCommandExecutor<in TInput, TOutput> where TInput : class
    {
        #region methods

        /// <summary>
        /// Execute command
        /// </summary>
        /// <param name="command"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<TOutput> ExecuteAsync(TInput command, HttpContext context);

        #endregion
    }
}
