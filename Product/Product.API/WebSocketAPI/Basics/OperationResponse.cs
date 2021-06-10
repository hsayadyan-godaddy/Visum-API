using Newtonsoft.Json;
using Product.API.WebSocketAPI.Helpers;
using System.Net;


namespace Product.API.WebSocketAPI.Basics
{
    public class OperationResponse
    {
        #region properties

        public WSRequestType RequestType { get; set; }
        public OperationResponseStatus Status { get; set; }
        public string ResultJSContent { get; set; }

        #endregion //properties

        public OperationResponse()
        {
            Status = new OperationResponseStatus();
        }

        public OperationResponse(object value, OperationResponseStatus status, WSRequestType RequestType = WSRequestType.Subscribe)
        {
            Status = status;
            ResultJSContent = value.GetJson();
        }

        public OperationResponse(OperationRequest value, HttpStatusCode statusCode = HttpStatusCode.OK, string errorMessage = null)
        {
            RequestType = value.RequestType;
            Status = new OperationResponseStatus(statusCode, errorMessage);
        }
    }
}
