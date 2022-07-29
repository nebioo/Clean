using System.Linq;
using System.Reflection;
using ApplicationContract.Response;
using ApplicationService.Common;
using Domain.Common;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Attribute;

public class ValidateModelAttribute : ActionFilterAttribute
{
    private readonly IAppLogger _appLogger;

    public ValidateModelAttribute(IAppLogger appLogger)
    {
        _appLogger = appLogger;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var exceptionFeature = context.HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionFeature != null)
            {
                _appLogger.Exception(exceptionFeature.Error, MethodBase.GetCurrentMethod(), GetErrorMessageFromContext(context));
            }

            var response = new ResponseBase()
            {
                Success = false,
                MessageCode = ApplicationMessage.InvalidParameter,
                Message = ApplicationMessage.InvalidParameter.Message(),
                UserMessage = ApplicationMessage.InvalidParameter.UserMessage()
            };
            context.Result = new OkObjectResult(response);
        }
    }

    private static string GetErrorMessageFromContext(ActionExecutingContext context)
    {
        var errorMessage = string.Empty;

        var errorCollectionList = context.ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
        foreach (var errorCollection in errorCollectionList)
        {
            foreach (var error in errorCollection)
            {
                if (error.Exception != null)
                    errorMessage += error.Exception.Message;
                else
                    errorMessage += error.ErrorMessage;
            }
        }

        return errorMessage;
    }
}