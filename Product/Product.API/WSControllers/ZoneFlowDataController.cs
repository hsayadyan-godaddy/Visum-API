using Product.API.Models;
using Product.API.WebSocketAPI.Basics;
using Product.API.WebSocketAPI.CustomAttributes;

namespace Product.API.WSControllers
{
    [WSController]
    public class ZoneFlowDataController
    {
        [WSMethod(typeof(OperationMethodMetadata))]
        public void SubscribeZoneFlowData(string wellName, WSContext context)
        {
            context.ResultCallback(new OperationResponse(new OperationMethodMetadata { MethodName ="www" }, new OperationResponseStatus()));
            
        }
    }
}
