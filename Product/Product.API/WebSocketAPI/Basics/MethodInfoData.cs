using System.Collections.Generic;
using System.Reflection;

namespace Product.API.WebSocketAPI.Basics
{
    public class MethodInfoData
    {
        public MethodInfo MethodInfo { get; set; }
        public List<WSOperationMethodParams> Params { get; set; }
    }
}
