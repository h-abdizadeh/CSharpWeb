
using SheypoorChi.Core.ViewModels;
using SheypoorChi.DataLayer.Models;

namespace SheypoorChi.Core.Interface;

public interface IShopping:IDisposable
{
    public Task<Factor> AddFactor(AddShoppingVM shopping);
    public Task<Factor> GetFactor(Guid userId);
    public Task<Factor> GetFactor(int factorId);
    public Task<int> SetFactor(Guid userId);//set totall price
    public Task<int> SetFactor(int factorId,string refId,bool isPay=false);//set is pay state
}
