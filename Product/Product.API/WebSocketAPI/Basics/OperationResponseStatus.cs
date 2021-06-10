using System.Net;

namespace Product.API.WebSocketAPI.Basics
{
    public class OperationResponseStatus
    {
        #region properties

        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool ServiceUnavailable { get; set; }

        #endregion //properties

        public OperationResponseStatus()
        {
            StatusCode = (int)HttpStatusCode.OK;
        }

        public OperationResponseStatus(HttpStatusCode statusCode, string errorMessage)
        {
            StatusCode = (int)statusCode;
            ErrorMessage = errorMessage;
            ServiceUnavailable = statusCode == HttpStatusCode.ServiceUnavailable;
        }
    }
}
