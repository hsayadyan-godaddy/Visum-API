using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Text;

namespace Product.API.WebSocketAPI.Helpers
{
    /// <summary>
    /// Extension JSON serialization
    /// </summary>
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


        #endregion //constants

        /// <summary>
        /// Get Bytes Array
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] GetBytesArray(this string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }

        /// <summary>
        /// Get Bytes Array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] GetBytesArray<T>(this T value)
        {
            return GetJson(value).GetBytesArray();
        }

        /// <summary>
        /// Get Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static string GetJson<T>(this T value, Func<JsonSerializerSettings> settings = null)
        {
            return JsonConvert.SerializeObject(value, GetSettings(settings));
        }

        /// <summary>
        /// Get Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T GetObject<T>(this byte[] value)
        {
            return GetObject<T>(GetString(value));
        }

        /// <summary>
        /// Get String
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetString(this byte[] value)
        {
            return Encoding.UTF8.GetString(value);
        }
        /// <summary>
        /// Get Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>

        public static T GetObject<T>(this string value)
        {
            return GetObjectFromJson<T>(value);
        }

        /// <summary>
        /// Get Object From Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
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
#if DEBUG
            Console.WriteLine(errorArgs?.ErrorContext?.Error);
#endif

            errorArgs.ErrorContext.Handled = true;
        }

        #endregion //privates

    }
}
