using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Product.API.WebSocketAPI.Basics
{

    public class OperationMethodMetadata
    {
        public bool OneTime { get; set; }
        public string MethodName { get; set; }
        public string ReturnModelName { get; set; }
        public string ReturnModel { get; set; }
        public string RequestModel { get; set; }
        public string RequestTypesModel { get; set; }
        public List<WSOperationMethodParams> Params { get; set; }

        public OperationMethodMetadata Test { get; set; }

        public OperationRequest TEST3 { get; set; }
    }
}
