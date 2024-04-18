using SheypoorChi.DataLayer.Models;
using Microsoft.AspNetCore.Http;
namespace SheypoorChi.Core.Interface;

public interface IAdmin:IDisposable
{
    public Task<List<Product>> GetProducts();
    public Task<List<Product>> GetProducts(bool notShow=false);
    public Task<List<Group>> GetGroups();

    public Task<bool> AddProduct(Product product,IFormFile imgFile);

}
