using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using PracticeWebAPIDemo.WebApi.Infrastructure.Models;

namespace PracticeWebAPIDemo.Infrastructure.ActionFilters
{
    public class CustomValidatorAttribute : ActionFilterAttribute
    {
        private readonly Type _validatorType;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomValidatorAttribute"/> class.
        /// </summary>
        /// <param name="validatorType">Type of the validator.</param>
        public CustomValidatorAttribute(Type validatorType)
        {
            this._validatorType = validatorType;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var parameters = context.ActionArguments;
            if (parameters.Count <= 0)
            {
                await base.OnActionExecutionAsync(context, next);
            }

            var parameter = parameters.FirstOrDefault();
            if (parameter.Value == null)
            {
                context.Result = new BadRequestObjectResult("未輸入 Parameter");
            }

            var validator = Activator.CreateInstance(this._validatorType) as IValidator;
            var validationContext = new ValidationContext<object>(parameter.Value);
            var validationResult = await validator.ValidateAsync(validationContext);

            if (validationResult.IsValid.Equals(false))
            {
                var errorResultOutput = new ErrorInfoModel
                {
                    Id = new Guid(),
                    Method = $"{context.HttpContext.Request.Path}.{context.HttpContext.Request.Method}",
                    Status = "VaildationError",
                    Errors = validationResult.Errors.Select
                    (
                        item => new ErrorDetail()
                        {
                            ErrorCode = StatusCodes.Status400BadRequest.ToString(),
                            Message = $"PropertyName: {item.PropertyName}",
                            Description = item.ErrorMessage
                        }
                    ).ToList(),
                };

                context.Result = new BadRequestObjectResult(errorResultOutput);
            }

            await base.OnActionExecutionAsync(context, next);
        }
    }
}
