using System.Collections.Generic;

namespace Product.API.WebSocketAPI.Basics
{
    /// <summary>
    /// Operation metadata
    /// </summary>
    public class OperationMetadata
    {
        /// <summary>
        /// Operation source name
        /// </summary>
        public string OperationSource { get; set; }
        /// <summary>
        /// Operation supported methods
        /// </summary>
        public List<OperationMethodMetadata> OperationMethods { get; set; }
    }
}
