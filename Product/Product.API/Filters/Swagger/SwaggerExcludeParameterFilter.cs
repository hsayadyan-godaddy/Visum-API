using Microsoft.OpenApi.Models;
using Product.DataModels.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Product.API.Filters.Swagger
{
    public class SwaggerExcludeParameterFilter : IOperationFilter
    {
        #region publics

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var types = context?.ApiDescription?.ParameterDescriptions?
                .Where(desc => desc.ParameterDescriptor != null)?
                .Select(desc => desc.ParameterDescriptor.ParameterType);

            foreach (var type in types)
            {

                var exclude = type.GetProperties().Where(x => x.GetCustomAttributes(typeof(SwaggerExcludeAttribute), true)?.Length > 0);

                foreach (var itm in exclude)
                {
                    var rem = operation.Parameters.FirstOrDefault(x => string.Compare(x.Name, itm.Name, true) == 0);
                    if (rem != null)
                    {
                        operation.Parameters.Remove(rem);
                    }
                }
            }
        }

        #endregion
    }
}
