using System.Collections.Generic;
using System.Reflection;

namespace Product.API.WebSocketAPI.Basics
{
    /// <summary>
    /// Method info data
    /// </summary>
    public class MethodInfoData
    {
        /// <summary>
        /// Ref. method info
        /// </summary>
        public MethodInfo MethodInfo { get; set; }
        /// <summary>
        /// Method parameters info
        /// </summary>
        public List<WSOperationMethodParams> Params { get; set; }
    }
}
