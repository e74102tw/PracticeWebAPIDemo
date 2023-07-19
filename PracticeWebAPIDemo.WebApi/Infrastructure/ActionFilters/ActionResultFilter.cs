using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PracticeWebAPIDemo.WebApi.Infrastructure.Models;
using System.Globalization;
using System.Xml.Serialization;
using Newtonsoft.Json.Linq;

namespace PracticeWebAPIDemo.WebApi.Infrastructure.ActionFilters
{
    public class ActionResultFilter : IAsyncActionFilter, IFilterMetadata
    {
        private static string StatusSuccess => "Success";

        private static string StatusError => "Error";

        public ActionResultFilter()
        {
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ActionExecutedContext actionExecutedContext = await next();
            ObjectResult objectResult = actionExecutedContext.Result as ObjectResult;
            if (objectResult == null)
            {
                return;
            }

            object value = objectResult.Value;
            if (value is HttpResponseMessage)
            {
                return;
            }

            bool flag = context.Controller.GetType().Name.Equals("ActionController", StringComparison.OrdinalIgnoreCase).Equals(obj: false);
            Endpoint endpoint = context.HttpContext.GetEndpoint();
            string method = context.HttpContext.Request.Method;

            ErrorDetail errorDetail = objectResult.Value as ErrorDetail;

            if (errorDetail != null)
            {
                ErrorInfoModel errorInfo = new ErrorInfoModel
                {
                    Id = new Guid(),
                    Method = $"{context.HttpContext.Request.Path}.{context.HttpContext.Request.Method}",
                    Status = "VaildationError",
                    Errors = new List<ErrorDetail>()
                    { 
                        errorDetail
                    }
                };
                actionExecutedContext.Result = new ObjectResult(errorInfo)
                {
                    StatusCode = objectResult.StatusCode
                };
                return;
            }

            if (objectResult.StatusCode < 400 && objectResult.StatusCode >= 200)
            {
                SuccessResultOutputModel<object> value2 = new SuccessResultOutputModel<object>
                {
                    Id = Guid.NewGuid(),
                    Method = $"{context.HttpContext.Request.Path}.{method}",
                    Status = objectResult.StatusCode.ToString(),
                    Data = objectResult.Value
                };
                actionExecutedContext.Result = new ObjectResult(value2)
                {
                    StatusCode = objectResult.StatusCode
                };
                return;
            }
            else if (objectResult.StatusCode >= 400)
            {
                ErrorResultOutputModel errorResultOutput = new ErrorResultOutputModel
                {
                    Id = Guid.NewGuid(),
                    Method = $"{context.HttpContext.Request.Path}.{method}",
                    Status = objectResult.StatusCode.ToString(),
                    Data = objectResult.Value
                };
                actionExecutedContext.Result = new ObjectResult(errorResultOutput)
                {
                    StatusCode = objectResult.StatusCode
                };
            }
        }
    }
}
