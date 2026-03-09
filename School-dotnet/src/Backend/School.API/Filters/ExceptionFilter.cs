using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using School.Communication.Responses;
using School.Exceptions;
using School.Exceptions.ExceptionsBase;

namespace School.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is SchoolException schoolException)
            HandleProjectException(schoolException, context);
        else
            ThrowUnknowException(context);
    }

    private static void HandleProjectException(SchoolException schoolException, ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)schoolException.GetStatusCode();
        context.Result = new ObjectResult(new ResponseErrorJson(schoolException.GetErrorMessages()));
    }

    private static void ThrowUnknowException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesException.UNKNOWN_ERROR));
    }
}