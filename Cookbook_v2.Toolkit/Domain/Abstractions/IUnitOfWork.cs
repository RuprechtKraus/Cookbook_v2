using System;
using System.Threading.Tasks;

namespace Cookbook_v2.Toolkit.Domain.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveAsync();
    }
}
