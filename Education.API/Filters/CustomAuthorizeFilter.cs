using Education.Core.DTOs;
using Education.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Education.API.Filters
{
    public class CustomAuthorizeFilter : ActionFilterAttribute
    {
        public Guid PermissionId { get; set; }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            List<ErrorDto> list = new List<ErrorDto>();
            list.Add(new ErrorDto { Error = "Bu işlemi yapma yetkiniz bulunmamaktadır." });
            context.Result = new UnauthorizedObjectResult(new ErrorListResponse(list));
        }
    }
}
