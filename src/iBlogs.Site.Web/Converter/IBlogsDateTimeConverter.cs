using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace iBlogs.Site.Web.Converter
{
    public class SfaDateTimeConverter : DateTimeConverterBase
    {
        //private static readonly DateTime StartTime = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1), TimeZoneInfo.Local);

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            try
            {
                writer.WriteValue(value == null ? "" : ((DateTime)value).ToString("O"));
            }
            catch (Exception)
            {
                writer.WriteValue("");
            }
        }

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            if (!DateTime.TryParse(reader.Value.ToString(), out var value))
                return null;

            return value;
        }


        /// <inheritdoc />
        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}
