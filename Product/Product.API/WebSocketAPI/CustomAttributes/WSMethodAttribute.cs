using System;

namespace Product.API.WebSocketAPI.CustomAttributes
{
    /// <summary>
    /// WebSocket Method Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class WSMethodAttribute : Attribute
    {
        /// <summary>
        /// Declare that methid will not return response, one-time execution
        /// </summary>
        public bool OneTime { get; }
        /// <summary>
        /// Declare expected operation return type
        /// </summary>
        public Type ReturnType { get; }

        /// <summary>
        /// Attribute for WebSoocket method operation. 
        /// </summary>
        /// <param name="returnType">Declaration of expected data type that will be returned in callback to user</param>
        /// <param name="oneTime">
        ///  Declaration of the oneTime = true means that method will not support subscribe/unsubscribe.
        ///  Expected one time execution with the response or without
        /// </param>
        public WSMethodAttribute(Type returnType = null, bool oneTime = false)
        {
            OneTime = oneTime;
            ReturnType = returnType;
        }
    }
}
