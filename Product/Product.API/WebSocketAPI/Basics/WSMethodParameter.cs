using Newtonsoft.Json.Linq;

namespace Product.API.WebSocketAPI.Basics
{
    /// <summary>
    /// WebSocket method parameters info
    /// </summary>
    public struct WSMethodParameter
    {
        /// <summary>
        /// Parameter name
        /// </summary>
        public string Name;
        /// <summary>
        /// Parameter value
        /// </summary>
        public JToken Value;

        /// <summary>
        /// Create new instance
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public WSMethodParameter(string name, JToken value)
        {
            Name = name;
            Value = value;
        }
    }
}
