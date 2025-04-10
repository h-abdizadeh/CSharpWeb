using Microsoft.AspNetCore.Mvc;
using FardaOnlineShop.Models;
using FardaOnlineShop.Data;
using Microsoft.EntityFrameworkCore;

namespace FardaOnlineShop.Controllers;

public class HomeController : Controller
{
    private readonly DatabaseContext _context;

    public HomeController(DatabaseContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var products =
             _context.Products
             .Include(g => g.Group)
             .Where(p => !p.NotShow /*== false*/)
             .ToList();

        return View(products);
    }


    public async Task<IActionResult> ProductInfo(int? id)//productId
    {
        if (id is null)
            return RedirectToAction(nameof(Index));

        var product =
            _context.Products
            .Include(g => g.Group)
            .FirstOrDefault(p => p.Id == id && !p.NotShow);

        if (product is null)
            return RedirectToAction(nameof(Index));


        ViewBag.Related = await RelatedProducts(product);
        return View(product);
    }

    [Route("products/{id?}/{name?}")]
    public IActionResult ProductGroup(int? id, string? name)
    {
        if (id is null)
            return RedirectToAction("Index");

        var products =
            _context.Products
            .Include(g => g.Group)
            .Where(p => p.GroupId == id && !p.NotShow)
            .ToList();

        if (products is null)
            return RedirectToAction(nameof(Index));

        return View(products);
    }


    public async Task<List<Product>> RelatedProducts(Product product)
    {
        var products =
            await _context.Products
                  .Include(g => g.Group)
                  .Where(p => p.Id != product.Id &&
                         p.GroupId == product.GroupId &&
                         !p.NotShow)
                  .Take(4)
                  .ToListAsync();

        return products;
    }

    public IActionResult Search(string search)
    {
        if(search is null)
            return RedirectToAction(nameof(Index));

        var input = search.Trim();
        if (string.IsNullOrEmpty(input))
            return RedirectToAction(nameof(Index));

        ViewBag.Search = input;
        var result =
            _context.Products.Include(g => g.Group)
            .Where(p => !p.NotShow &&
            (p.Name.ToLower().Contains(input.ToLower()) ||
            p.Description.ToLower().Contains(input.ToLower())))
            .OrderByDescending(p => p.SellOff)
            .ToList();

        //return View(result)

        //select
        result = result.Select(p => new Product
        {
            Id = p.Id,
            Name = p.Name,
            Img = p.Img,
            Price = p.Price
        }).ToList();

        return View(result);
    }
}
