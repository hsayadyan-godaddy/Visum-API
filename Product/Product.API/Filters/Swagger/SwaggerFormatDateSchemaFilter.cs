using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Product.DataModels.Attributes;
using Product.DataModels.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Product.API.Filters.Swagger
{
    public class SwaggerFormatDateSchemaFilter : ISchemaFilter
    {
        #region publics

        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var type = context.Type;
            if (schema?.Properties == null || type == null)
                return;



            var tmp = type.GetProperties().Where(x => x.GetCustomAttributes(typeof(Newtonsoft.Json.JsonConverterAttribute), true)?.Length > 0);

            foreach (var itm in tmp)
            {

                var args = itm.CustomAttributes.Where(x => x.AttributeType == typeof(Newtonsoft.Json.JsonConverterAttribute))
                                .FirstOrDefault(x => x.ConstructorArguments.FirstOrDefault().Value.ToString() == (typeof(JsonUnixDateFormatConverter).ToString()));

                if (args != null)
                {
                    var k = schema.Properties.FirstOrDefault(x => string.Compare(x.Key, itm.Name, true) == 0);
                    var kdef = default(KeyValuePair<string, OpenApiSchema>);
                    if (!kdef.Equals(k))
                    {
                        k.Value.Example = new OpenApiLong(DateTime.Now.ToUnix());
                    }
                }
            }
        }

        #endregion
    }
}
