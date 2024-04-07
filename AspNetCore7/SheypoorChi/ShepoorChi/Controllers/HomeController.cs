using Microsoft.AspNetCore.Mvc;
using SheypoorChi.Core.Interface;

namespace ShepoorChi.Controllers;

public class HomeController : Controller
{
    IAdmin _admin;

    public HomeController(IAdmin admin)
    {
        _admin = admin;
    }

    public async Task<IActionResult> Index()
    {
        //get all groups
        var groups = await _admin.GetGroups();
        //filter groups (notShow false)
        //ViewBag.Groups = groups.Where(g => g.NotShow == false);
        //ViewBag.Groups = groups.Where(g => g.NotShow is false);
        ViewBag.Groups = groups.Where(g => !g.NotShow);

        return View();
    }
}
