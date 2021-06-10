using System;
using System.Threading;

namespace Product.API.WebSocketAPI.Basics
{
    public struct WSContext
    {
        public string ConnectionID;
        public string SessionToken;
        public Func<OperationResponse, bool> ResultCallback;
        public CancellationToken Cancellation;

        public WSContext(string sessionToken, string connectionID, Func<OperationResponse, bool> callback, CancellationToken cancellation)
        {
            ConnectionID = connectionID;
            SessionToken = sessionToken;
            ResultCallback = callback;
            Cancellation = cancellation;
        }
    }
}

