using SheypoorChi.DataLayer.Models;
using Microsoft.AspNetCore.Http;
using SheypoorChi.Core.ViewModels;

namespace SheypoorChi.Core.Interface;

public interface IAdmin : IDisposable
{
    public Task<List<Product>> GetProducts();
    public Task<List<Product>> GetProducts(int groupId);
    public Task<List<Product>> GetProducts(int groupId, int productId);
    public Task<List<Product>> GetProducts(bool notShow = false);
    public Task<List<Product>> GetProducts(string search);
    public Task<List<Group>> GetGroups();
    public Task<Group> GetGroup(int groupId);

    public Task<bool> AddProduct(Product product, IFormFile imgFile);

    public Task<Product> GetProduct(int id);

    ///account actions
    public Task<User> GetUser(LoginVM login);
    public Task<User> GetUser(string mobile);

    public Task<bool> AddUser(RegisterVM register);

    //public Task<bool> SetUserDetail(UserDetail userDetail);
    public Task<bool> SetUserDetail(UserInfo userDetail);

}
