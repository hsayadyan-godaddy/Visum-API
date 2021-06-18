using Microsoft.OpenApi.Models;
using Product.API.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;


namespace Product.API.Filters.Swagger
{
    internal class SwaggerExcludeSchemaFilter : ISchemaFilter
    {
        #region publics

        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {

            var type = context.Type;
            if (schema?.Properties == null || type == null)
                return;

            var exclude = type.GetProperties().Where(x => x.GetCustomAttributes(typeof(SwaggerExcludeAttribute), true)?.Length > 0);

            foreach (var itm in exclude)
            {
                var k = schema.Properties.Keys.FirstOrDefault(x => string.Compare(x, itm.Name, true) == 0);
                if (k != null)
                {
                    schema.Properties.Remove(k);
                }
            }
        }

        #endregion
    }
}
