using Newtonsoft.Json.Serialization;
using System;

namespace iBlogs.Site.Web.Converter
{
    public class BlogsContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonContract CreateContract(Type objectType)
        {
            var contract = base.CreateContract(objectType);

            if (objectType == typeof(DateTime) ||
                objectType == typeof(DateTimeOffset) ||
                objectType == typeof(DateTime?))
            {
                contract.Converter = new BlogsDateTimeConverter();
            }

            return contract;
        }
    }
}