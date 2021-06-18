using Product.API.WebSocketAPI.Basics;
using System.Collections.Generic;

namespace Product.API.WebSocketAPI.Abstraction
{
    /// <summary>
    /// Operation Executor
    /// </summary>
    public interface IOperationExecutor
    {
        /// <summary>
        /// Invoke Operation
        /// </summary>
        /// <param name="value"></param>
        /// <param name="operationCallback"></param>
        void InvokeOperation(OperationRequest value, WSContext operationCallback);

        /// <summary>
        /// Available/supported operations
        /// </summary>
        List<OperationMetadata> SupportedOperations { get; }
        /// <summary>
        /// Known models as JSON
        /// </summary>
        List<string> KnownModels { get; }
    }
}
