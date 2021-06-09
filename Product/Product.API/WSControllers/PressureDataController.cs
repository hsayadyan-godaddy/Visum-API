using Product.API.WebSocketAPI.Basics;
using Product.API.WebSocketAPI.CustomAttributes;
using Product.DataModels;
using System.Collections.Generic;

namespace Product.API.WSControllers
{
    [WSController]
    public class PressureDataController
    {


        [WSMethod(typeof(PressureData))]
        public void SubscribePressureData(string wellName, string key, WSContext context)
        {
#warning //TODO implement collabacks from simulator

            context.ResultCallback(
              new OperationResponse(new PressureData { Data = new List<ValueTime>(), Label="PSI" }, new OperationResponseStatus()));
        }
    }
}
