namespace Product.API.WebSocketAPI.Abstraction
{
    public interface ITokenValidator
    {
        bool ValidateToken(string value);
    }
}
