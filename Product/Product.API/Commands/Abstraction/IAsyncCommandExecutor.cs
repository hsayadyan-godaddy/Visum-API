using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Product.API.Commands.Abstraction
{
    public interface IAsyncCommandExecutor<in TInput> where TInput : class
    {
        #region methods

        Task<bool> ExecuteAsync(TInput command, HttpContext context);

        #endregion
    }

    public interface IAsyncCommandExecutor<in TInput, TOutput> where TInput : class
    {
        #region methods

        Task<TOutput> ExecuteAsync(TInput command, HttpContext context);

        #endregion
    }
}
