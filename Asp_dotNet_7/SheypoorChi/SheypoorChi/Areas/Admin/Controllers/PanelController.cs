using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShepoorChi.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
//[UserRoleAttribute("admin")]
[UserRole("admin")]
public class PanelController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
