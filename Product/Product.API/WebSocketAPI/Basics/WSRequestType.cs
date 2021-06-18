namespace Product.API.WebSocketAPI.Basics
{
    /// <summary>
    /// WebSocket supported operation types
    /// </summary>
    public enum WSRequestType
    {
        /// <summary>
        /// Call as function, response is not required
        /// </summary>
        OneTime = 0,
        /// <summary>
        /// Subscribe on callback
        /// </summary>
        Subscribe,
        /// <summary>
        /// Unsubscribe on callback
        /// </summary>
        Unsubscribe
    }
}
