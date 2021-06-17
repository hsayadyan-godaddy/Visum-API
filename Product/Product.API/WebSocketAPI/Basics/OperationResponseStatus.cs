using System.Net;

namespace Product.API.WebSocketAPI.Basics
{
    /// <summary>
    /// WebSocket operation response status
    /// </summary>
    public class OperationResponseStatus
    {
        #region properties

        /// <summary>
        /// Default OK status
        /// </summary>
        public static OperationResponseStatus OK { get; } = new OperationResponseStatus();

        /// <summary>
        /// Status code
        /// </summary>
        public int StatusCode { get; set; }
        /// <summary>
        /// Error message
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// Is service unavailable
        /// </summary>
        public bool ServiceUnavailable { get; set; }

        #endregion //properties

        /// <summary>
        /// Create new instance
        /// </summary>
        public OperationResponseStatus()
        {
            StatusCode = (int)HttpStatusCode.OK;
        }

        /// <summary>
        /// Create new instance
        /// </summary>
        public OperationResponseStatus(HttpStatusCode statusCode, string errorMessage)
        {
            StatusCode = (int)statusCode;
            ErrorMessage = errorMessage;
            ServiceUnavailable = statusCode == HttpStatusCode.ServiceUnavailable;
        }
    }
}
