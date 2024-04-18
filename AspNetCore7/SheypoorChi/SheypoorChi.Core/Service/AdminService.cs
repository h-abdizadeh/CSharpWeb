

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SheypoorChi.Core.Classes;
using SheypoorChi.Core.Interface;
using SheypoorChi.DataLayer.Context;
using SheypoorChi.DataLayer.Models;

namespace SheypoorChi.Core.Service;

public class AdminService : IAdmin
{
    private readonly DatabaseContext _context;

    public AdminService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<bool> AddProduct(Product product, IFormFile imgFile)
    {
        try
        {
            //upload product image
            product.Img = await new ImageClass().SaveProductImg(imgFile);

            //save product data
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return true;

        }
        catch (Exception e)
        {

            Console.WriteLine($"===> add product error ===> {e}");
            return false;
        }
    }

    public void Dispose()
    {
        if (_context is not null)//!=
        {
            _context.Dispose();
        }
    }

    public async Task<List<Group>> GetGroups()
    {
        var groups = await _context.Groups.ToListAsync();
        return groups;
    }

    public async Task<List<Product>> GetProducts()
    {
        var products = await _context.Products.ToListAsync();
        return products;
    }

    public async Task<List<Product>> GetProducts(bool notShow = false)
    {
        var products =
            await _context.Products.Where(p => p.NotShow == notShow).ToListAsync();

        return products;
    }
}
