using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Api.DtoBuilders;
using TestTask.Api.Models.Errors;

namespace TestTask.Api.Filters
{
    public class ErrorFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;
            ObjectResult result = null;
            switch (context.Exception)
            {
                case BaseApiException baseApiException:
                    var dto = ExceptionDtoBuilder.Map(baseApiException);
                    result = new ObjectResult(dto);
                    result.StatusCode = (int)baseApiException.HttpCode;
                    break;

                default:
                    var ddto = ExceptionDtoBuilder.Map(context.Exception);
                    result = new ObjectResult(ddto);
                    result.StatusCode = 500;
                    break;
            }
            context.Result = result;

        }
    }
}
