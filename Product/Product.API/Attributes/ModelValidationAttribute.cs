using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Reflection;

namespace Product.API.Attributes
{
    /// <summary>
    /// Mode lValidation Attribute
    /// </summary>
    public class ModelValidationAttribute : ActionFilterAttribute
    {
        #region publics

        /// <summary>
        /// Handler
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var modelState = context.ModelState;


            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;

            if (descriptor != null)
            {
                var parameters = descriptor.MethodInfo.GetParameters();

                foreach (var parameter in parameters)
                {
                    if (context.ActionArguments.ContainsKey(parameter.Name))
                    {
                        var argument = context.ActionArguments[parameter.Name];
                        EvaluateValidationAttributes(parameter, argument, context.ModelState);
                    }
                }
            }

            if (modelState != null && modelState.IsValid)
                return;


            context.Result = new ContentResult
            {
                Content = PrepareMessage(modelState),
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }

        #endregion

        #region privates

        private static string PrepareMessage(ModelStateDictionary dictionary)
        {
            var modelErrors = dictionary?.Values.SelectMany(modelState => modelState.Errors);
            if (modelErrors == null)
                return "Empty model is unexpected";

            try
            {
                return string.Join("\r\n", modelErrors.Select(t => t.ErrorMessage));
            }
            catch (JsonSerializationException)
            {
                return "Unexpected request model";
            }
        }

        private void EvaluateValidationAttributes(ParameterInfo parameter, object argument, ModelStateDictionary modelState)
        {
            var validationAttributes = parameter.CustomAttributes;

            foreach (var attributeData in validationAttributes)
            {
                var attributeInstance = CustomAttributeExtensions.GetCustomAttribute(parameter, attributeData.AttributeType);

                var validationAttribute = attributeInstance as ValidationAttribute;

                if (validationAttribute != null)
                {
                    var isValid = validationAttribute.IsValid(argument);
                    if (!isValid)
                    {
                        modelState.AddModelError(parameter.Name, validationAttribute.FormatErrorMessage(parameter.Name));
                    }
                }
            }
        }

        #endregion
    }
}
