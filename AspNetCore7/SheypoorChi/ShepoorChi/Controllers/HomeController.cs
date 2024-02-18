using Microsoft.AspNetCore.Mvc;

namespace ShepoorChi.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
