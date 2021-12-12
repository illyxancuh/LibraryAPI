using LibraryAPI.BusinessLogic.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace LibraryAPI.Host
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            HandleExceptionAsync(context);
            context.ExceptionHandled = true;
        }

        private static void HandleExceptionAsync(ExceptionContext context)
        {
            var exception = context.Exception;

            switch (exception)
            {
                case NotFoundException:
                    SetExceptionResult(context, exception, HttpStatusCode.NotFound, true);
                    break;
                case AccessDeniedException:
                    SetExceptionResult(context, exception, HttpStatusCode.Forbidden, true);
                    break;
                case ValidationException:
                    SetExceptionResult(context, exception, HttpStatusCode.BadRequest, true);
                    break;
                default:
                    SetExceptionResult(context, exception, HttpStatusCode.InternalServerError, false);
                    break;
            }
        }

        private static void SetExceptionResult(ExceptionContext context,
                                               Exception exception,
                                               HttpStatusCode code,
                                               bool requiresMessage)
        {
            if (requiresMessage)
            {
                context.Result = new JsonResult(new { message = exception.Message })
                {
                    StatusCode = (int)code
                };

                return;
            }

            context.Result = new StatusCodeResult((int)code);
        }
    }
}
