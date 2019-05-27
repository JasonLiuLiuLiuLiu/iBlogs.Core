using System;
using iBlogs.Site.Core.Common.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace iBlogs.Site.Web.Converter
{
    public class SfaDateTimeConverter : DateTimeConverterBase
    {
       
        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            try
            {
                writer.WriteValue(value == null ? "" : ((DateTime)value).ToUnixTimestamp().ToString());
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
