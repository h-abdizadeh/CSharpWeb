using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Razor;
using NuGet.Packaging.Signing;
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
        var products = await _admin.GetProducts(notShow: false);

        return View(products);
    }

    public async Task<IActionResult> ProductInfo(int id)//id -> productId
    {
        var product = await _admin.GetProduct(id);

        //redirect to index if not found product
        if (product is null) return RedirectToAction(nameof(Index));

        ViewBag.Related =
            await _admin.GetProducts(groupId: product.GroupId, productId: product.Id);


        return View(product);
    }

    public async Task<IActionResult> Products(int id)//id -> groupId
    {
        var products = await _admin.GetProducts(id);

        if (products is null) return RedirectToAction(nameof(Index));

        ViewBag.groupName = (await _admin.GetGroup(id)).GroupName;
        return View(products);

    }


    public async Task<IActionResult> Search(string search)
    {
        ViewBag.Search = search;

        if (string.IsNullOrEmpty(search))
            return RedirectToAction(nameof(Index));

        var products = await _admin.GetProducts(search);
        

        //return View(products);
        //don't creat view -> return data to another View(viewName,data)
        return View("products", products);
    }
}
