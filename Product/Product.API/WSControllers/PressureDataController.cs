using Product.API.WebSocketAPI.Basics;
using Product.API.WebSocketAPI.CustomAttributes;

namespace Product.API.WSControllers
{
    [WSController]
    public class PressureDataController
    {


        [WSMethod(typeof(string))]
        public void SubscribePressureData(string wellName, string key, WSContext context)
        {
            var b = context.ResultCallback(new OperationResponse("All is ok", new OperationResponseStatus()));

        }
    }
}
