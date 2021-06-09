using System;

namespace Product.API.WebSocketAPI.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class WSMethodAttribute : Attribute
    {
        public bool OneTime { get; }
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
