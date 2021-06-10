using Product.API.WebSocketAPI.Basics;
using Product.API.WebSocketAPI.CustomAttributes;
using Product.DataModels;

namespace Product.API.WSControllers
{
    [WSController]
    public class RateDataController
    {
        [WSMethod(typeof(RateData))]
        public void SubscribeRateData(string wellName, string key, WSContext context)
        {
#warning //TODO implement collabacks from simulator

            context.ResultCallback(
                new OperationResponse(new RateData(), new OperationResponseStatus()));

        }
    }
}
