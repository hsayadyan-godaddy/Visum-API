using System.Collections.Generic;

namespace Product.API.WebSocketAPI.Basics
{
    public class WSOperationInstance
    {
        public object Instance { get; set; }
        public Dictionary<string, MethodInfoData> OperationMethods { get; set; }
    }
}
