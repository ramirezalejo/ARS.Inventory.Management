using ARS.Inventory.Management.Infrastructure.Repository.Context;
using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Infrastructure.Interfaces;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ARS.Inventory.Management.Infrastructure.Repository.Repositories
{
    public class PurchaseRepository : BaseRepository<Purchase>
    {
        public PurchaseRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override Purchase GetById(Expression<Func<Purchase, bool>> filter)
        {
            var dbResult = dbSet.Where(filter).Include(c => c.Product).Include(c => c.Product.Supplier).Include(c => c.User).SingleOrDefault();
            return dbResult;
        }

        public async Task<Purchase> GetByIdAsync(Expression<Func<Purchase, bool>> filter)
        {
            var dbResult = await dbSet.Include(c => c.Product).Include(c => c.Product.Supplier).Include(c => c.User).FirstAsync(filter);
            return dbResult;
        }

        public IEnumerable<Purchase> GetAll()
        {
            return dbSet.Include(c => c.Product).Include(c => c.Product.Supplier).Include(c => c.User).AsEnumerable();
        }

        public IEnumerable<Purchase> GetAll(Expression<Func<Purchase, bool>> filter)
        {
            return dbSet.Where(filter).Include(c => c.Product).Include(c => c.Product.Supplier).Include(c => c.User).AsEnumerable();
        }

    }
}
