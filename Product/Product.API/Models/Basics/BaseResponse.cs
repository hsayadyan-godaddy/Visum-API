using Product.API.Attributes;
using Product.API.Models.Error;

namespace Product.API.Models.Basics
{
    /// <summary>
    /// Implementation of the base response
    /// </summary>
    public class BaseResponse : IBaseResponse
    {
        /// <summary>
        /// Errror message field
        /// </summary>
        [SwaggerExclude]
        public ServerError Error { get; private set; }
    }
}
