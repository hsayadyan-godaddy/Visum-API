using System.Collections.Generic;

namespace Product.API.WebSocketAPI.Basics
{
    /// <summary>
    /// Operation Method Metadata
    /// </summary>
    public class OperationMethodMetadata
    {
        /// <summary>
        /// Is one-time operation or subscribe/unsubscribe
        /// </summary>
        public bool OneTime { get; set; }
        /// <summary>
        /// Method name
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// Return JSON model name
        /// </summary>
        public string ReturnModelName { get; set; }
        /// <summary>
        /// Return JSON model structure
        /// </summary>
        public string ReturnModel { get; set; }
        /// <summary>
        /// Request JSON model
        /// </summary>
        public string RequestModel { get; set; }
        /// <summary>
        /// Request types model
        /// </summary>
        public string RequestTypesModel { get; set; }
        /// <summary>
        /// Parameters info
        /// </summary>
        public List<WSOperationMethodParams> Params { get; set; }
    }
}
