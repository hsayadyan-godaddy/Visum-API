using System.Collections.Generic;

namespace Product.API.WebSocketAPI.Basics
{
    /// <summary>
    /// WebSocket operation request
    /// </summary>
    public class OperationRequest
    {
        #region properties

        /// <summary>
        /// Connection ID
        /// </summary>
        public string ConnectionId { get; set; }
        /// <summary>
        /// Operation source name
        /// </summary>
        public string OperationSource { get; set; }
        /// <summary>
        /// Method name
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// Request type
        /// </summary>
        public WSRequestType RequestType { get; set; }
        /// <summary>
        /// Method parameters
        /// </summary>
        public Dictionary<string, string> MethodParameters { get; set; }

        #endregion // properties

        /// <summary>
        /// returns the same identifier for same parameterts if Suscribe or Unsubscribe
        /// </summary>
        public int RequestId
        {
            get
            {
                if (RequestType == WSRequestType.Subscribe || RequestType == WSRequestType.Unsubscribe)
                {
                    return BasicHash();
                }
                else
                {
                    unchecked
                    {
                        var hash = BasicHash();
                        hash = hash * 23 + RequestType.GetHashCode();
                        return hash;
                    }
                }
            }
        }

        /// <summary>
        /// Create new instance
        /// </summary>
        public OperationRequest()
        {
            MethodParameters = new Dictionary<string, string>();
        }

        #region privates

        private int BasicHash()
        {
            unchecked
            {
                var hash = 17;

                hash = hash * 23 + CalculateDeterminatedStringHash(ConnectionId);
                hash = hash * 23 + CalculateDeterminatedStringHash(OperationSource);
                hash = hash * 23 + CalculateDeterminatedStringHash(MethodName);

                if (MethodParameters != null) // could be a null if not a struct
                {
                    foreach (var key in MethodParameters.Keys)
                    {
                        hash = hash * 23 + CalculateDeterminatedStringHash(key);
                        hash = hash * 23 + CalculateDeterminatedStringHash(MethodParameters[key]);
                    }
                }

                return hash;
            }
        }

        private int CalculateDeterminatedStringHash(string value)
        {
            unchecked
            {
                var hash = 17;
                if (!string.IsNullOrEmpty(value))
                {
                    var tmp = value.ToLower();
                    for (var i = 0; i < tmp.Length; i++)
                    {
                        hash = hash * 23 + tmp[i].GetHashCode();
                    }
                }
                return hash;
            }
        }

        #endregion //privates
    }
}
