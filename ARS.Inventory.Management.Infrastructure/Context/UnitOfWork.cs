using Microsoft.EntityFrameworkCore;
using ARS.Inventory.Management.Infrastructure.Repository.Context;
using ARS.Inventory.Management.Infrastructure.Interfaces;

namespace ARS.Inventory.Management.Infrastructure.Repository.Infrastucture
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InventoryDbContext _dbContext;

        public UnitOfWork(InventoryDbContext inventoryDbContext)
        {
            _dbContext = inventoryDbContext;
        }

        public DbContext Db
        { get { return _dbContext; } }

        public void Dispose()
        {
        }
    }
}
