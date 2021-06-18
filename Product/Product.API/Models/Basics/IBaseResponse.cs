using Product.API.Models.Error;

namespace Product.API.Models.Basics
{
    /// <summary>
    /// Base response interface
    /// </summary>
    public interface IBaseResponse
    {
        /// <summary>
        /// Error message field
        /// </summary>
        ServerError Error { get; }
    }
}
