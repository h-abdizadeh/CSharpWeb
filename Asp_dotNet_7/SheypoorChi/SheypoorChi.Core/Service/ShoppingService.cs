

using Microsoft.EntityFrameworkCore;
using SheypoorChi.Core.Interface;
using SheypoorChi.Core.ViewModels;
using SheypoorChi.DataLayer.Context;
using SheypoorChi.DataLayer.Models;

namespace SheypoorChi.Core.Service;

public class ShoppingService : IShopping
{
    private readonly DatabaseContext _context;
    public ShoppingService(DatabaseContext context)
    {
        _context = context;
    }
    public async Task<Factor> AddFactor(AddShoppingVM shopping)
    {
        try
        {
            //get user from Admin service GetUser(mobile)

            //get factor
            var factor =
                await _context.Factors
                .FirstOrDefaultAsync(f => f.UserId == shopping.UserId && f.IsPay == false);

            if (factor is not null)
            {
                //add to open factor details
                var newDetail = new FactorDetail()
                {
                    FactorId = factor.Id,
                    ProductId = shopping.ProductId,
                    DetailCount = shopping.ShoppingCount
                };

                await _context.FactorDetails.AddAsync(newDetail);
                await _context.SaveChangesAsync();

                factor.Details.Add(newDetail);
                return factor;
            }
            else
            {
                //create new factor
                var newFactor = new Factor()
                {
                    Id = new Random().Next(10000, 100000),
                    UserId = shopping.UserId,
                    IsPay = false,
                    Status = new FactorStatusVM().StatusArray[0]
                };
                await _context.Factors.AddAsync(newFactor);

                //add factorDetail
                var newDetail = new FactorDetail()
                {
                    FactorId = newFactor.Id,
                    ProductId = shopping.ProductId,
                    DetailCount = shopping.ShoppingCount
                };
                await _context.FactorDetails.AddAsync(newDetail);

                await _context.SaveChangesAsync();

                return newFactor;
            }
        }
        catch (Exception error)
        {
            Console.WriteLine($"add factor error => {error}");
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

    public async Task<Factor> GetFactor(Guid userId)
    {
        var factor =
            await _context.Factors
            .Include(f => f.Details)
            .Include("Details.Product")
            .FirstOrDefaultAsync(f => f.UserId == userId && !f.IsPay);


        return factor;
    }

    public async Task<Factor> GetFactor(int factorId)
    {
        var factor = await _context.Factors.Include(f => f.Details).FirstOrDefaultAsync(f => f.Id == factorId);

        return factor;
    }

    public async Task<int> SetFactor(Guid userId)
    {
        var factor =
            await _context.Factors.Include(f => f.Details).FirstOrDefaultAsync(f => f.UserId == userId && !f.IsPay);

        if (factor is null) return 0;

        var factorPrice = 0;

        foreach (var item in factor.Details)
        {
            var product = await _context.Products.FindAsync(item.ProductId);
            if (product is null) continue;

            //var productPrice = product.Price * (100 - product.SellOff) / 100;

            factorPrice += item.DetailCount * product.Price;
        }

        factor.TotalPrice = factorPrice;

        await _context.SaveChangesAsync();

        return factor.Id;

    }

    public async Task<int> SetFactor(int factorId,string refId, bool isPay = false)
    {
        var factor=await _context.Factors.FindAsync(factorId);

        if (factor is null) return 0;

        factor.IsPay = isPay;
        factor.PayInfo = refId;

        if (isPay)
            factor.Status=new FactorStatusVM().StatusArray[1];

        await _context.SaveChangesAsync();
        return factor.Id;
    }
}
