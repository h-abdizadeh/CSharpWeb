
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using SheypoorChi.DataLayer.Context;

namespace ShepoorChi.Areas.Admin.Controllers;

internal class UserRoleAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    private readonly string _roleName;
    private readonly DatabaseContext _context=new DatabaseContext();
    public UserRoleAttribute(string roleName)
    {
        _roleName = roleName;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var identity = context.HttpContext.User.Identity;

        if (identity.IsAuthenticated)
        {
            var userMobile = identity.Name;

            var user =
                 _context.Users
                .FirstOrDefault(u => u.UserName == userMobile &&
                                          u.Role.RoleName == _roleName);

            if (user == null)
                //context.Result = new RedirectResult("~/account/login");
                context.Result = new RedirectResult("~/profile/index");
        }
        else
        {
            context.Result = new RedirectResult("~/account/login");
        }
    }
}