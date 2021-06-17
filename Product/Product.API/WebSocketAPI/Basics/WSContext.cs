using System;
using System.Threading;

namespace Product.API.WebSocketAPI.Basics
{
    /// <summary>
    /// WebSocket context
    /// </summary>
    public struct WSContext
    {
        /// <summary>
        /// Connection ID
        /// </summary>
        public string ConnectionId;
        /// <summary>
        /// Session access token
        /// </summary>
        public string SessionToken;
        /// <summary>
        /// Result callback delegate
        /// </summary>
        public Func<OperationResponse, bool> ResultCallback;
        /// <summary>
        /// Cancelation token
        /// </summary>
        public CancellationToken Cancellation;
        /// <summary>
        /// Request type
        /// </summary>
        public WSRequestType RequestType { get; set; }

        /// <summary>
        /// Create new instance
        /// </summary>
        /// <param name="sessionToken"></param>
        /// <param name="connectionId"></param>
        /// <param name="requestType"></param>
        /// <param name="callback"></param>
        /// <param name="cancellation"></param>
        public WSContext(string sessionToken, string connectionId, WSRequestType requestType, Func<OperationResponse, bool> callback, CancellationToken cancellation)
        {
            ConnectionId = connectionId;
            SessionToken = sessionToken;
            ResultCallback = callback;
            Cancellation = cancellation;
            RequestType = requestType;
        }
    }
}

