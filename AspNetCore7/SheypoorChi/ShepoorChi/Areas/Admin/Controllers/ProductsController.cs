using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SheypoorChi.Core.Interface;
using SheypoorChi.DataLayer.Models;

namespace ShepoorChi.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductsController : Controller
{
    IAdmin _admin;

    public ProductsController(IAdmin admin)
    {
        _admin = admin;
    }

    //[HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _admin.GetProducts();

        return View(products);
    }

    public async Task<IActionResult> Create()
    {
        var groups = await _admin.GetGroups();
        ViewBag.Groups = new SelectList(groups, "Id", "GroupName");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        if (ModelState.IsValid)
        {
            //add product

            return RedirectToAction(nameof(Index));
        }


        ViewBag.Groups = 
            new SelectList(await _admin.GetGroups(), "Id", "GroupName");

        return View(product);
    }
}
