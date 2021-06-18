using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Product.API.WebSocketAPI.Abstraction
{
    internal interface IWebSocketHandler
    {
        Task Handle(HttpContext context);
    }
}
