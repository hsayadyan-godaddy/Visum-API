using Newtonsoft.Json.Linq;
using Product.API.WebSocketAPI.Basics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Threading;
namespace Product.API.WebSocketAPI.Helpers
{
    /// <summary>
    /// Response handler
    /// </summary>
    public class ResponseQueue
    {
        private const int QUEUE_MAX_SIZE = 20;
        private const int QUEUE_REGULAR_SIZE = 10;

        private readonly object _locker = new object();
        private readonly Dictionary<int, Queue<(WSRequest, OperationResponse)>> _responses = new Dictionary<int, Queue<(WSRequest, OperationResponse)>>();
        private readonly WebSocket _client;
        private readonly CancellationToken _cancellationToken;

        /// <summary>
        /// Create new instance
        /// </summary>
        /// <param name="client"></param>
        /// <param name="cancellationToken"></param>
        public ResponseQueue(WebSocket client, CancellationToken cancellationToken)
        {
            _client = client;
            _cancellationToken = cancellationToken;

            var th = new Thread(() =>
            {
                var activeID = -1;

                using (var reseter = new AutoResetEvent(false))
                {
                    while (true)
                    {
                        WSRequest wsRequest = null;
                        OperationResponse response = null;
                        lock (_locker)
                        {
                            if (_responses.Count > 0)
                            {
                                var keys = _responses.Keys.ToList();
                                var idx = keys.IndexOf(activeID);
                                if (idx >= 0)
                                {
                                    if (_responses[activeID].Count == 0)
                                    {
                                        _responses.Remove(activeID);
                                    }
                                }

                                if (_responses.Count > 0)
                                {
                                    if (idx + 1 >= keys.Count)
                                    {
                                        activeID = keys[0];
                                    }
                                    else
                                    {
                                        activeID = keys[idx + 1];
                                    }

                                    var tmp = _responses[activeID].Dequeue();
                                    wsRequest = tmp.Item1;
                                    response = tmp.Item2;
                                }
                            }
                        }

                        if (response != null)
                        {
                            var err = WebSocketHandler.Broken(client);

                            if (!err)
                            {
                                var wsResponse = new WSResponse
                                {
                                    SequenceId = wsRequest.SequenceId,
                                    StatusCode = response.Status.StatusCode,
                                    StatusMessage = !string.IsNullOrEmpty(response.Status.ErrorMessage) ? response.Status.ErrorMessage : Errors.GetMessage((HttpStatusCode)response.Status.StatusCode),
                                    OperationSource = wsRequest.OperationSource,
                                    MethodName = wsRequest.MethodName,
                                    Response = response.ResultJSContent != null ? JToken.Parse(response.ResultJSContent) : null
                                };


                                var sendbytes = wsResponse.GetBytesArray();

                                var offset = 0;
                                var len = Math.Min(WebSocketHandler.BUFFER_SIZE, sendbytes.Length);

                                while (!WebSocketHandler.Broken(client))
                                {
                                    var eom = offset + len == sendbytes.Length;

                                    try
                                    {
                                        client.SendAsync(new ArraySegment<byte>(sendbytes, offset, len), WebSocketMessageType.Text, eom, _cancellationToken)
                                        .GetAwaiter()
                                        .GetResult();
                                    }
                                    catch
                                    {
                                        err = true;
                                        return;
                                    }

                                    offset = offset + WebSocketHandler.BUFFER_SIZE;
                                    len = Math.Min(WebSocketHandler.BUFFER_SIZE, sendbytes.Length - offset);

                                    if (eom)
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            if (_cancellationToken.IsCancellationRequested)
                            {
                                break;
                            }
                            else
                            {
                                reseter.WaitOne(20);
                            }
                        }
                    }
                }
            })
            {
                IsBackground = true
            };
            th.Start();
        }

        /// <summary>
        /// Enqueue operation request
        /// </summary>
        /// <param name="operationRequestId"></param>
        /// <param name="wsRequest"></param>
        /// <param name="response"></param>
        public void Enqueue(int operationRequestId, WSRequest wsRequest, OperationResponse response)
        {
            var key = operationRequestId;
            var pair = (wsRequest, response);
            lock (_locker)
            {
                if (!_responses.ContainsKey(key))
                {
                    _responses.Add(key, new Queue<(WSRequest, OperationResponse)>());
                }

                var queue = _responses[key];
                queue.Enqueue(pair);

                if (queue.Count >= QUEUE_MAX_SIZE)
                {
                    while (queue.Count > QUEUE_REGULAR_SIZE)
                    {
                        queue.Dequeue();
                    }
                }
            }
        }
    }
}
