using Product.API.WebSocketAPI.Basics;
using Product.API.WebSocketAPI.CustomAttributes;
using Product.DataModels;

namespace Product.API.WSControllers
{
    [WSController]
    public class ZoneFlowDataController
    {
        [WSMethod(typeof(ZoneFlowTick))]
        public void SubscribeZoneFlowData(string wellName, WSContext context)
        {

            context.ResultCallback(new OperationResponse(
                new ZoneFlowTick { Gas = 1, Oil = 2, Water = 3, Time = 1623239258 },
                new OperationResponseStatus()));
        }
    }
}
