using Newtonsoft.Json.Linq;

namespace Product.API.WebSocketAPI.Basics
{
    public class WSResponse
    {
        public string SequenceID { get; set; }
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public string OperationSource { get; set; }
        public string MethodName { get; set; }
        public JToken Response { get; set; }
    }
}
