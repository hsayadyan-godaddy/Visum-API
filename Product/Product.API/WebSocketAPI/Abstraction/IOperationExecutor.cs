using Product.API.WebSocketAPI.Basics;
using System.Collections.Generic;

namespace Product.API.WebSocketAPI.Abstraction
{
    public interface IOperationExecutor
    {
        void InvokeOperation(OperationRequest value, WSContext operationCallback);

        List<OperationMetadata> SupportedOperations { get; }
        List<string> KnownModels { get; }
    }
}
