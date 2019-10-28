using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace uadec.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var jsonResult = new JsonResult(
                new { 
                    error = context.Exception.Message,
                    message = "Este es un mensaje de error"
                });
            jsonResult.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
            
            context.ExceptionHandled = true;
            context.Result = jsonResult;

        }
    }
}
