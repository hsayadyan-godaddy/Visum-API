using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Product.API.WebSocketAPI.Basics
{
    public class WSRequest
    {
        public string SequenceID { get; set; }
        public string OperationSource { get; set; }
        public string MethodName { get; set; }
        public WSRequestType RequestType { get; set; }
        public List<WSMethodParameter> MethodParameters { get; set; }
    }

    public struct WSMethodParameter
    {
        public string Name;
        public JToken Value;

        public WSMethodParameter(string name, JToken value)
        {
            Name = name;
            Value = value;
        }
    }
}
