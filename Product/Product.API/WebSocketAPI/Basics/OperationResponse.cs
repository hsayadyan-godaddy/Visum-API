using Product.API.WebSocketAPI.Helpers;
using System.Net;


namespace Product.API.WebSocketAPI.Basics
{
    /// <summary>
    /// Websocket operation response
    /// </summary>
    public class OperationResponse
    {
        #region properties

        /// <summary>
        /// Request type
        /// </summary>
        public WSRequestType RequestType { get; set; }
        /// <summary>
        /// Response status
        /// </summary>
        public OperationResponseStatus Status { get; set; }
        /// <summary>
        /// Result JSON content
        /// </summary>
        public string ResultJSContent { get; set; }

        #endregion //properties

        /// <summary>
        /// Create new instance
        /// </summary>
        public OperationResponse()
        {
            Status = new OperationResponseStatus();
        }

        /// <summary>
        /// Create new instance
        /// </summary>
        public OperationResponse(object value, OperationResponseStatus status, WSRequestType requestType = WSRequestType.Subscribe)
        {
            Status = status;
            ResultJSContent = value.GetJson();
            RequestType = requestType;
        }

        /// <summary>
        /// Create new instance
        /// </summary>
        public OperationResponse(OperationRequest value, HttpStatusCode statusCode = HttpStatusCode.OK, string errorMessage = null)
        {
            RequestType = value.RequestType;
            Status = new OperationResponseStatus(statusCode, errorMessage);
        }
    }
}
