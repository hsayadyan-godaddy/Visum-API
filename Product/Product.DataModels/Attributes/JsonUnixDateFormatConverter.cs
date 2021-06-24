using Newtonsoft.Json;
using Product.DataModels.Extensions;
using System;
using System.Globalization;

namespace Product.DataModels.Attributes
{
    public class JsonUnixDateFormatConverter : JsonConverter<DateTime>
    {
        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var str = reader.Value.ToString().Trim();
            if (string.IsNullOrEmpty(str))
            {
                return DateTime.MinValue;
            }
            else
            {
                var tmp = long.Parse(reader.Value.ToString().Trim(), NumberStyles.Number, CultureInfo.InvariantCulture);
                return tmp.FromUnix();
            }
        }

        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            if (value == DateTime.MinValue)
            {
                writer.WriteValue(string.Empty);
            }
            else
            {
                var unixTime = value.ToUnix();
                writer.WriteValue(unixTime);
            }
        }
    }
}
