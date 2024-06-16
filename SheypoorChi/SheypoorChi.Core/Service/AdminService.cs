

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SheypoorChi.Core.Classes;
using SheypoorChi.Core.Interface;
using SheypoorChi.Core.ViewModels;
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

    public async Task<bool> AddUser(RegisterVM register)
    {
        try
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                RoleId = await CheckUserRoleId(),
                UserName = register.UserName,
                UserPassword =
                await new Security().GetHash(register.Password),
                IsActive = true
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception error)
        {
            Console.WriteLine($"ERROR (AddUser) ==> {error}");
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

    public async Task<Group> GetGroup(int groupId)
    {
        var group = await _context.Groups.FindAsync(groupId);

        return group;
    }

    public async Task<List<Group>> GetGroups()
    {
        var groups = await _context.Groups.ToListAsync();
        return groups;
    }

    public async Task<Product> GetProduct(int id)
    {
        //1 find
        //var product = await _context.Products.FindAsync(id);


        //2 firstOrDefault <-> singleOrDefault
        var product =
            await _context.Products
            .Include(p => p.Group)
            .FirstOrDefaultAsync(p => p.Id == id);

        //3 where - select
        //var product =
        //    await _context.Products
        //    .Where(p => p.Id == id)
        //    .Select(p => new Product()
        //    {
        //        Id = p.Id,
        //        Title = p.Title,
        //        Group = p.Group,
        //        //Group = new Group() { GroupName = p.Group.GroupName },
        //    }).
        //    FirstOrDefaultAsync();

        return product;

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

    public async Task<List<Product>> GetProducts(int groupId)
    {
        var products = await _context.Products
            .Include(p => p.Group)
            .Where(p => p.GroupId == groupId && !p.NotShow)
            .ToListAsync();

        return products;
    }

    public async Task<List<Product>> GetProducts(int groupId, int productId)
    {
        var products = await _context.Products
             .Include(p => p.Group)
             .Where(p => p.GroupId == groupId && !p.NotShow && p.Id != productId)
             .ToListAsync();

        return products;
    }

    public async Task<List<Product>> GetProducts(string search)
    {
        var products =
            await _context.Products
            .Where(p => !p.NotShow &&
            (p.Title.Contains(search) || p.Description.Contains(search)))
            .OrderByDescending(p => p.SubmitDate)
            .ToListAsync();

        return products;
    }

    public async Task<User> GetUser(LoginVM login)
    {
        //convert user input password to hashed password
        var hashPass = await new Security().GetHash(login.Password);

        var user =
            await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.UserName == login.UserName &&
            u.UserPassword == hashPass);

        return user;
    }

    public async Task<User> GetUser(string mobile)
    {
        var user =
            await _context.Users.FirstOrDefaultAsync(u => u.UserName == mobile);

        return user;
    }

    private protected async Task<Guid> CheckUserRoleId()
    {
        var userRole =
            await _context.Roles
            .FirstOrDefaultAsync(r => r.RoleName == "user");

        if (userRole is not null)
        {
            return userRole.Id;
        }

        //add user role
        var role = new Role()
        {
            Id = Guid.NewGuid(),
            RoleName = "user",
            RoleTitle = "کاربر",
        };

        await _context.Roles.AddAsync(role);
        await _context.SaveChangesAsync();

        return role.Id;

    }
}
