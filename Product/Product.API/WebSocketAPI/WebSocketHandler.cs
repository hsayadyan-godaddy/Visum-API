using Microsoft.AspNetCore.Http;
using Product.API.WebSocketAPI.Abstraction;
using Product.API.WebSocketAPI.Basics;
using Product.API.WebSocketAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Product.API.WebSocketAPI
{
    public class WebSocketHandler : IWebSocketHandler
    {
        #region constants

        public const int BUFFER_SIZE = ushort.MaxValue;

        #endregion //constants

        #region members

        public readonly ITokenValidator _tokenValidator;
        public readonly IOperationExecutor _operationExecutor;

        #endregion //members

        #region ctor

        public WebSocketHandler(ITokenValidator tokenValidator, IOperationExecutor operationExecutor)
        {
            _tokenValidator = tokenValidator;
            _operationExecutor = operationExecutor;
        }

        #endregion //ctor

        #region publics

        public async Task Handle(HttpContext context)
        {
            var authToken = string.Empty;

            if (context.Request.Query.ContainsKey(CommonKeys.SESSION_TOKEN))
            {
                authToken = context.Request.Query[CommonKeys.SESSION_TOKEN].ToString();
            }

            if (_tokenValidator.ValidateToken(authToken))
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    var client = await context.WebSockets.AcceptWebSocketAsync();

                    try
                    {
                        await Handle(authToken, client, context);
                    }
                    catch
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    }
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                }
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
        }

        #endregion //publics

        #region privates

        private async Task Handle(string authToken, WebSocket client, HttpContext context)
        {
            var buffer = new byte[BUFFER_SIZE];
            var tmp = new byte[0];
            WebSocketReceiveResult result;
            var cancellationSource = new CancellationTokenSource();
            var responseQueue = new ResponseQueue(client, cancellationSource.Token);

            while (true)
            {
                result = null;

                try
                {
                    result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationSource.Token);
                }
                catch
                {
                    break;
                }

                if (result.CloseStatus.HasValue || Broken(client))
                {
                    break;
                }

                ExtendBytes(buffer, result.Count, ref tmp);

                if (result.EndOfMessage)
                {
                    var wsRequest = tmp.GetObject<WSRequest>();
                    tmp = new byte[0];

                    var operationRequest = GetRequest(wsRequest, context);

                    if (operationRequest != null)
                    {
                        var resultCallback = new Func<OperationResponse, bool>((response) =>
                        {
                            var err = Broken(client);
                            var tokenValid = _tokenValidator.ValidateToken(authToken);

                            var keepConnection = !err && tokenValid;

                            if (keepConnection)
                            {
                                responseQueue.Enqueue(operationRequest.RequestID, wsRequest, response);
                            }
                            else
                            {
                                cancellationSource.Cancel();
                                client.Dispose();
                            }

                            return keepConnection;
                        });

                        var callbackResponse = new WSContext(authToken, operationRequest.ConnectionID, resultCallback, cancellationSource.Token);
                        _operationExecutor.InvokeOperation(operationRequest, callbackResponse);
                    }
                }
            }

            cancellationSource.Cancel();
            client.Dispose();
        }

        public static bool Broken(WebSocket client)
        {
            var err = client.CloseStatus.HasValue || client.State == WebSocketState.Aborted
                       || client.State == WebSocketState.Closed
                       || client.State == WebSocketState.CloseReceived
                       || client.State == WebSocketState.CloseSent;

            return err;
        }

        private OperationRequest GetRequest(WSRequest value, HttpContext context)
        {
            OperationRequest ret = null;
            if (value != null)
            {
                var parameters = new Dictionary<string, string>();
                if (value.MethodParameters != null)
                {
                    foreach (var itm in value.MethodParameters)
                    {
                        parameters.Add(itm.Name.ToLower(), itm.Value.ToString());
                    }
                }

                ret = new OperationRequest()
                {
                    ConnectionID = context.Connection.Id,
                    RequestType = value.RequestType,
                    OperationSource = value.OperationSource,
                    MethodName = value.MethodName,
                    MethodParameters = parameters
                };
            }

            return ret;
        }

        private void ExtendBytes(byte[] value, int count, ref byte[] destination)
        {
            var cnt = destination.Length;
            var arr = new byte[cnt + count];
            Buffer.BlockCopy(destination, 0, arr, 0, cnt);
            Buffer.BlockCopy(value, 0, arr, cnt, count);
            destination = arr;
        }

        #endregion //privates
    }
}
