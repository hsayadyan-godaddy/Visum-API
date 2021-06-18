using System.Collections.Generic;

namespace Product.API.WebSocketAPI.Basics
{
    /// <summary>
    /// WebSocket Operation INstance
    /// </summary>
    public class WSOperationInstance
    {
        /// <summary>
        /// Instance object
        /// </summary>
        public object Instance { get; set; }
        /// <summary>
        /// Operation methods
        /// </summary>
        public Dictionary<string, MethodInfoData> OperationMethods { get; set; }
    }
}
