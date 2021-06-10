using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Text;

namespace Product.API.WebSocketAPI.Helpers
{
    public static class DataSerializerExt
    {
        #region constants

        private static readonly JsonSerializerSettings _jsFormattedDefaults = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Include,
            Error = HandleDeserializationError,

            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            MissingMemberHandling = MissingMemberHandling.Ignore,
        };

        private static readonly JsonSerializerSettings _jsNonFormattedDefaults = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.None,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Include,
            Error = HandleDeserializationError,

            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            MissingMemberHandling = MissingMemberHandling.Ignore,
        };

        public class UserTypeEnumConverter : StringEnumConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType.IsEnum;
            }

            public override object ReadJson(JsonReader reader,
                                            Type objectType,
                                            object existingValue,
                                            JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null)
                {
                    var isNullable = (Nullable.GetUnderlyingType(objectType) != null);
                    if (!isNullable)
                    {
                        throw new JsonSerializationException();
                    }
                    return null;
                }

                var token = JToken.Load(reader);
                var value = token.ToString();

                object ret = null;

                try
                {
                    ret = Enum.Parse(objectType, value, true);
                }
                catch (Exception e)
                {
#warning implement logger
                    Console.WriteLine(e);
                }

                if (ret != null)
                {
                    return ret;
                }
                else
                {
                    return base.ReadJson(reader, objectType, existingValue, serializer);
                }
            }
        }

        #endregion //constants


        public static byte[] GetBytesArray(this string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }

        public static byte[] GetBytesArray<T>(this T value)
        {
            return GetJson(value).GetBytesArray();
        }

        public static string GetJson<T>(this T value, Func<JsonSerializerSettings> settings = null)
        {
            return JsonConvert.SerializeObject(value, GetSettings(settings));
        }

        public static T GetObject<T>(this byte[] value)
        {
            return GetObject<T>(GetString(value));
        }

        public static string GetString(this byte[] value)
        {
            return Encoding.UTF8.GetString(value);
        }

        public static T GetObject<T>(this string value)
        {
            return GetObjectFromJson<T>(value);
        }

        public static T GetObjectFromJson<T>(string json, Func<JsonSerializerSettings> settings = null)
        {
            return string.IsNullOrEmpty(json)
                ? default(T)
                : JsonConvert.DeserializeObject<T>(json, GetSettings(settings));
        }

        #region privates

        private static JsonSerializerSettings GetSettings(Func<JsonSerializerSettings> value, bool structureFormatting = true)
        {
            if (structureFormatting)
            {
                return value?.Invoke() ?? _jsFormattedDefaults;
            }
            else
            {
                return value?.Invoke() ?? _jsNonFormattedDefaults;
            }
        }

        private static void HandleDeserializationError(object sender, ErrorEventArgs errorArgs)
        {
#warning implement logger
            Console.WriteLine(errorArgs?.ErrorContext?.Error);

            errorArgs.ErrorContext.Handled = true;
        }

        #endregion //privates

    }
}
