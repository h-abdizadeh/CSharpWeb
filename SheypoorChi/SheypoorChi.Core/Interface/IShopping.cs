
using SheypoorChi.DataLayer.Models;

namespace SheypoorChi.Core.Interface;

public interface IShopping:IDisposable
{
    public Task<Factor> AddFactor(Guid userId, FactorDetail detail);
}
