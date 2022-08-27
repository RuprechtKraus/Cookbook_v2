using System;
using System.Threading.Tasks;

namespace Cookbook_v2.Domain.UoW.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveAsync();
    }
}
