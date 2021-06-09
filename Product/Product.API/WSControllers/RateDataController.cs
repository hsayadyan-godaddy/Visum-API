using Product.API.WebSocketAPI.Basics;
using Product.API.WebSocketAPI.CustomAttributes;

namespace Product.API.WSControllers
{
    [WSController]
    public class RateDataController
    {
        [WSMethod(typeof(string))]
        public void SubscribeRateData(string wellName, string key, WSContext context)
        {
            context.ResultCallback(new OperationResponse("All is ok", new OperationResponseStatus()));
            
        }
    }
}
