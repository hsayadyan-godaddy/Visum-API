using System.Collections.Generic;

namespace Product.API.WebSocketAPI.Basics
{
    public class OperationMetadata
    {
        public string OperationSource { get; set; }
        public List<OperationMethodMetadata> OperationMethods { get; set; }
    }
}
