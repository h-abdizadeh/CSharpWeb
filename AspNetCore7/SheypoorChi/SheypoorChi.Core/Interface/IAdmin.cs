using SheypoorChi.DataLayer.Models;
namespace SheypoorChi.Core.Interface;

public interface IAdmin:IDisposable
{
    public Task<List<Product>> GetProducts();
    public Task<List<Group>> GetGroups();

}
