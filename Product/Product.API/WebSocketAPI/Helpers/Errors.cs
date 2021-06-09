using System.Net;

namespace Product.API.WebSocketAPI.Helpers
{
    public static class Errors
    {
        public static string GetMessage(HttpStatusCode value)
        {
            string ret;

            switch (value)
            {
                case HttpStatusCode.OK:
                    ret = "OK";
                    break;
                case HttpStatusCode.NotFound:
                    ret = "Requested resource does not exist on the server";
                    break;
                case HttpStatusCode.BadRequest:
                    ret = "Request could not be understood by the server";
                    break;
                case HttpStatusCode.Forbidden:
                    ret = "The server refuses to fulfill the request";
                    break;
                case HttpStatusCode.ServiceUnavailable:
                    ret = "The server is temporarily unavailable";
                    break;
                case HttpStatusCode.InternalServerError:
                    ret = "Generic error has occurred on the server";
                    break;
                case HttpStatusCode.GatewayTimeout:
                    ret = "An intermediate proxy server timed out while waiting for a response from another origin server";
                    break;
                default:
                    ret = value.ToString();
                    break;
            }

            return ret;
        }
    }
}
