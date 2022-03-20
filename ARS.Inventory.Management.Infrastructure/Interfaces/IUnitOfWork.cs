
using Microsoft.EntityFrameworkCore;

namespace ARS.Inventory.Management.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Db { get; }
    }
}
