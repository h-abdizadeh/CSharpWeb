

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
}
