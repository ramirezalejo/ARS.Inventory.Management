using ARS.Inventory.Management.Infrastructure.Repository.Context;
using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Infrastructure.Interfaces;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ARS.Inventory.Management.Infrastructure.Repository
{
    public class ProductRepository : BaseRepository<Product>
    {
        public ProductRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override Product GetById(Expression<Func<Product, bool>> filter)
        {
            var dbResult = dbSet.Where(filter).Include(c => c.Supplier).Include(c => c.Category).SingleOrDefault();
            return dbResult;
        }

        public async Task<Product> GetByIdAsync(Expression<Func<Product, bool>> filter)
        {
            var dbResult = await dbSet.Include(c => c.Supplier).Include(c => c.Category).FirstAsync(filter);
            return dbResult;
        }

        public IEnumerable<Product> GetAll()
        {
            return dbSet.Include(c => c.Supplier).Include(c => c.Category).AsEnumerable();
        }

        public IEnumerable<Product> GetAll(Expression<Func<Product, bool>> filter)
        {
            return dbSet.Where(filter).Include(c => c.Supplier).Include(c => c.Category).AsEnumerable();
        }
    }
}
