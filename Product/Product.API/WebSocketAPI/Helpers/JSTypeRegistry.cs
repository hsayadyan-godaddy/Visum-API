using System;
using System.Collections.Generic;
using System.Text;

namespace Product.API.WebSocketAPI.Helpers
{
    public class JSTypeRegistry
    {
        private enum SimpleType
        {
            ComplexType,
            String,
            Number,
            Boolean
        }

        #region members

        private readonly Dictionary<string, string> _knownTypes = new Dictionary<string, string>();

        #endregion //members

        #region properties

        public List<string> KnownModels
        {
            get
            {
                var ret = new List<string>();
                foreach (var kn in _knownTypes)
                {
                    ret.Add($"{ToJsName(kn.Key)}\r\n{kn.Value}");
                }

                return ret;
            }
        }


        #endregion properties

        #region publics

        public string GetEnumAsModel(Type value)
        {
            var ret = string.Empty;

            if (value.IsEnum)
            {
                var sb = new StringBuilder();
                sb.Append($"{{\n");


                var values = Enum.GetValues(value);
                foreach (var val in values)
                {
                    sb.Append($"   {val}={(int)val},\n");
                }
                sb.Append("}");

                ret = sb.ToString();
            }

            return ret;
        }

        public string GetJsModel(Type value)
        {
            var tmpBase = JSBaseType(value);
            if (tmpBase != SimpleType.ComplexType)
            {
                return ToJsName(tmpBase.ToString());
            }
            else if (value.IsEnum)
            {
                return $" {string.Join(" | ", Enum.GetNames(value))} ";
            }
            else if (value.IsGenericType && value.GetGenericTypeDefinition() == typeof(List<>))
            {
                return $"[{GetJsModel(value.GetGenericArguments()[0])}]";
            }
            else if (Nullable.GetUnderlyingType(value) != null)
            {
                return $"null | {GetJsModel(Nullable.GetUnderlyingType(value))}";  
            }

            var model = "{";
            var props = value.GetProperties();

            var needComma = false;
            foreach (var itm in props)
            {
                var propType = itm.PropertyType;
                var jsBase = JSBaseType(propType);

                var jsType = jsBase == SimpleType.ComplexType ? itm.PropertyType.Name : jsBase.ToString();

                if (jsBase == SimpleType.ComplexType && !_knownTypes.ContainsKey(propType.Name))
                {
                    if (propType.IsEnum)
                    {
                        jsType = GetJsModel(propType);
                    }
                    else if (propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        jsType = GetJsModel(propType);
                    }
                    else if (Nullable.GetUnderlyingType(propType) != null)
                    {
                        jsType = GetJsModel(propType);
                    }
                    else
                    {
                        _knownTypes.Add(propType.Name, "{}");
                        var known = GetJsModel(propType);
                        _knownTypes[propType.Name] = known;
                    }
                }

                var commaSpace = needComma ? "," : "";
                model += $"{commaSpace}\r\n   {ToJsName(itm.Name)}: \"{ToJsName(jsType)}\"";
                needComma = true;
            }

            model += model.Length > 1 ? "\r\n}" : "}";

            return model;
        }

        #endregion //publics

        #region privates

        private SimpleType JSBaseType(Type value)
        {
            var ret = SimpleType.ComplexType;

            if (value.IsEnum) return ret;
            if (Nullable.GetUnderlyingType(value) != null) 
                return ret;
            

            switch (Type.GetTypeCode(value))
            {
                case TypeCode.Boolean:
                    ret = SimpleType.Boolean;
                    break;
                case TypeCode.Char:
                case TypeCode.SByte:
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    ret = SimpleType.Number;
                    break;
                case TypeCode.String:
                    ret = SimpleType.String;
                    break;
                default:
                    break;
            }

            return ret;
        }

        private string ToJsName(string value)
        {
            return $"{char.ToLower(value[0])}{value.Substring(1, value.Length - 1)}";
        }

        #endregion //privates
    }
}
