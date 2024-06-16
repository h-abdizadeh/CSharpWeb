

using Microsoft.EntityFrameworkCore;
using SheypoorChi.Core.Interface;
using SheypoorChi.DataLayer.Context;
using SheypoorChi.DataLayer.Models;

namespace SheypoorChi.Core.Service;

public class ShoppingService : IShopping
{
    private readonly DatabaseContext _context;
    public async Task<Factor> AddFactor(Guid userId, FactorDetail detail)
    {
        //get user from Adminservice GetUser(mobile)

        //get factor
        var factor = 
            await _context.Factors
            .FirstOrDefaultAsync(f=>f.UserId == userId && f.IsPay == false );

        if (factor is not null)
        {
            //add to open factor details
            var newDetail = new FactorDetail()
            {
                FactorId = factor.Id,
                ProductId = detail.ProductId,
                DetailCount = detail.DetailCount
            };

            await _context.FactorDetails.AddAsync(newDetail);
            await _context.SaveChangesAsync();

            factor.Details.Add(newDetail);
            return factor;
        }
        else
        {
            //create new factor
            return null;
        }
    }

    public void Dispose()
    {
        if (_context is not null)
        {
            _context.Dispose();
        }
    }
}
