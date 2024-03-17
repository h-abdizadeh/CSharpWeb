

using Microsoft.EntityFrameworkCore;
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

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public async Task<List<Product>> GetProducts()
    {
        var products = await _context.Products.ToListAsync();
        return products;
    }
}
