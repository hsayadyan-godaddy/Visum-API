using System;

namespace Product.API.Attributes
{
    /// <summary>
    /// Attribute to exclude property in swagger documentation
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SwaggerExcludeAttribute : Attribute
    {
    }
}
