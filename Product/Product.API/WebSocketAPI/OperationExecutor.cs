using Product.API.WebSocketAPI.Abstraction;
using Product.API.WebSocketAPI.Basics;
using Product.API.WebSocketAPI.CustomAttributes;
using Product.API.WebSocketAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Product.API.WebSocketAPI
{
    public class OperationExecutor : IOperationExecutor
    {
        #region members

        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, WSOperationInstance> _opServices = new Dictionary<string, WSOperationInstance>();
        private readonly List<OperationMetadata> _supportedOperations = new List<OperationMetadata>();
        private readonly JSTypeRegistry _jsModelReg = new JSTypeRegistry();

        public List<OperationMetadata> SupportedOperations => _supportedOperations;
        public List<string> KnownModels => _jsModelReg.KnownModels;

        #endregion //members

        #region ctor

        public OperationExecutor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            RegisterControllers();
        }

        #endregion //ctor

        #region publics

        public void InvokeOperation(OperationRequest value, WSContext operationCallback)
        {
            var operationNotFound = true;
            var opKey = value.OperationSource?.ToLower();
            var fnKey = value.MethodName?.ToLower();

            if (_opServices.ContainsKey(opKey))
            {
                var info = _opServices[opKey];

                if (info.OperationMethods.ContainsKey(fnKey))
                {
                    MethodInfo method = info.OperationMethods[fnKey].MethodInfo;

                    var methodParams = new List<object>();


                    foreach (var pxInfo in method.GetParameters())
                    {
                        if (pxInfo.ParameterType == typeof(WSContext))
                        {
                            methodParams.Add(operationCallback);
                        }
                        else
                        {
                            var key = pxInfo.Name;
                            if (value.MethodParameters.ContainsKey(key))
                            {
                                var val = value.MethodParameters[key];
                                var reqVal = Convert.ChangeType(val, pxInfo.ParameterType);
                                methodParams.Add(reqVal);
                            }
                            else
                            {
                                if (pxInfo.ParameterType.IsValueType)
                                {
                                    methodParams.Add(Activator.CreateInstance(pxInfo.ParameterType));
                                }
                                else
                                {
                                    methodParams.Add(null);
                                }
                            }
                        }

                    }

                    method.Invoke(info.Instance, methodParams.ToArray());
                    operationNotFound = false;
                }
            }

            if (operationNotFound)
            {
                operationCallback.ResultCallback?.Invoke(new OperationResponse(value, System.Net.HttpStatusCode.NotFound, "Operation is not found"));
            }
        }

        #endregion //publics

        #region privates

        private void RegisterControllers()
        {
            var asms = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var itm in asms)
            {
                var controllers = GetControllers(itm);

                foreach (var ctrl in controllers)
                {
                    var meta = new List<OperationMethodMetadata>();

                    var operationSource = ctrl.Name.ToLower();

                    _supportedOperations.Add(new OperationMetadata
                    {
                        OperationSource = operationSource,
                        OperationMethods = meta
                    });

                    var opMethods = new Dictionary<string, MethodInfoData>();

                    var instanceInfo = new WSOperationInstance
                    {
                        Instance = _serviceProvider.GetService(ctrl),
                        OperationMethods = opMethods
                    };

                    _opServices.Add(ctrl.Name.ToLower(), instanceInfo);

                    var operations = ctrl.GetMethods()
                                        .Where(x => x.IsPublic
                                                 && x.GetCustomAttributes<WSMethodAttribute>()
                                        .Any());

                    foreach (var methodInfo in operations)
                    {
                        var methodName = methodInfo.Name.ToLower();
                        var prmt = new List<WSOperationMethodParams>();
                        var attribute = methodInfo.GetCustomAttribute<WSMethodAttribute>();


                        var requestModelInstance = new WSRequest
                        {
                            MethodName = methodName,
                            OperationSource = operationSource,
                            SequenceID = "0123456ABCDEF",
                            RequestType = WSRequestType.Subscribe,
                            MethodParameters = new List<WSMethodParameter>()
                        };

                        opMethods.Add(methodName, new MethodInfoData
                        {
                            MethodInfo = methodInfo,
                            Params = prmt
                        });

                        foreach (var paramInfo in methodInfo.GetParameters())
                        {
                            if (paramInfo.ParameterType != typeof(WSContext))
                            {
                                var paramName = paramInfo.Name.ToLower();
                                var typeModel = _jsModelReg.GetJsModel(paramInfo.ParameterType);
                                prmt.Add(new WSOperationMethodParams
                                {
                                    Name = paramName,
                                    TypeModel = typeModel
                                });

                                requestModelInstance.MethodParameters.Add(new WSMethodParameter(paramName, typeModel));
                            }
                        }

                        meta.Add(new OperationMethodMetadata
                        {
                            MethodName = methodName,
                            OneTime = attribute.OneTime,
                            ReturnModelName = attribute.ReturnType.Name,
                            ReturnModel = _jsModelReg.GetJsModel(attribute.ReturnType),
                            RequestModel = requestModelInstance.GetJson(),
                            RequestTypesModel = _jsModelReg.GetEnumAsModel(typeof(WSRequestType)),
                            Params = prmt
                        });
                    }
                }
            }
        }

        private IEnumerable<Type> GetControllers(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(typeof(WSControllerAttribute), true).Length > 0)
                {
                    yield return type;
                }
            }
        }

        #endregion //privates
    }
}
