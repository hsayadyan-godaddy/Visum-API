using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Product.API.WebSocketAPI.Abstraction
{
    public interface IWebSocketHandler
    {
        Task Handle(HttpContext context);
    }
}
