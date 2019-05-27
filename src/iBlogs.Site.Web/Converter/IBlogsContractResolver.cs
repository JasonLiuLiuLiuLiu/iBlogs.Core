using System;
using Newtonsoft.Json.Serialization;

namespace iBlogs.Site.Web.Converter
{
    public class IBlogsContractResolver: CamelCasePropertyNamesContractResolver
    {
        protected override JsonContract CreateContract(Type objectType)
        {
            var contract = base.CreateContract(objectType);

            if (objectType == typeof(DateTime) ||
                objectType == typeof(DateTimeOffset) ||
                objectType == typeof(DateTime?))
            {
                contract.Converter = new SfaDateTimeConverter();
            }

            return contract;
        }
    }
}
