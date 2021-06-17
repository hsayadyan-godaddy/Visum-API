using System.Collections.Generic;

namespace Product.API.WebSocketAPI.Basics
{
    /// <summary>
    /// WebSocket request
    /// </summary>
    public class WSRequest
    {
        /// <summary>
        /// Sequence ID
        /// </summary>
        public string SequenceId { get; set; }
        /// <summary>
        /// Operation source name
        /// </summary>
        public string OperationSource { get; set; }
        /// <summary>
        /// Method mane
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// Request type
        /// </summary>
        public WSRequestType RequestType { get; set; }
        /// <summary>
        /// Method parameters
        /// </summary>
        public List<WSMethodParameter> MethodParameters { get; set; }
    }
}
