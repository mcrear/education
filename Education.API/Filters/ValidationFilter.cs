using Education.Core.DTOs;
using Education.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Education.API.Filters
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                List<ErrorDto> list = new List<ErrorDto>();
                IEnumerable<ModelError> modelErrors = context.ModelState.Values.SelectMany(x => x.Errors);
                modelErrors.ToList().ForEach(x =>
                {
                    list.Add(new ErrorDto { Error = x.ErrorMessage });
                });
                context.Result = new BadRequestObjectResult(new ErrorListResponse(list));
            }
        }
    }
}
