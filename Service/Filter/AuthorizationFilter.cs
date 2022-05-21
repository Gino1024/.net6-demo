using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Service.Filter
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isAllowAnonymous = (context.ActionDescriptor as ControllerActionDescriptor).MethodInfo.GetCustomAttributes<AllowAnonymousAttribute>().Any();
            if (isAllowAnonymous)
            {
                return;
            }
        }
    }
}
