namespace Product.API.WebSocketAPI.Abstraction
{
    /// <summary>
    /// TokenValidator
    /// </summary>
    internal interface ITokenValidator
    {
        /// <summary>
        /// Validate acces token
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool ValidateToken(string value);
    }
}
