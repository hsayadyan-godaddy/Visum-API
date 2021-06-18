using Newtonsoft.Json.Linq;

namespace Product.API.WebSocketAPI.Basics
{
    /// <summary>
    /// WebSocket response
    /// </summary>
    public class WSResponse
    {
        /// <summary>
        /// Sequence ID
        /// </summary>
        public string SequenceId { get; set; }
        /// <summary>
        /// Status code
        /// </summary>
        public int StatusCode { get; set; }
        /// <summary>
        /// Status message
        /// </summary>
        public string StatusMessage { get; set; }
        /// <summary>
        /// Operation source name
        /// </summary>
        public string OperationSource { get; set; }
        /// <summary>
        /// Method name
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// Method parameters
        /// </summary>
        public JToken Response { get; set; }
    }
}
