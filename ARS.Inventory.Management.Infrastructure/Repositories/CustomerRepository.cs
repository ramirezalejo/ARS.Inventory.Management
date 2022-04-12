using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Infrastructure.Interfaces;
using ARS.Inventory.Management.Infrastructure.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ARS.Inventory.Management.Infrastructure.Repository
{
    public class CustomerRepository : BaseRepository<Customer>
    {
        public CustomerRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override Customer GetById(Expression<Func<Customer, bool>> filter)
        {
            var dbResult = dbSet.Where(filter).Include(c => c.User).SingleOrDefault();
            return dbResult;
        }

        public async Task<Customer> GetByIdAsync(Expression<Func<Customer, bool>> filter)
        {
            var dbResult = await dbSet.Include(c => c.User).FirstAsync(filter);
            return dbResult;
        }

        public IEnumerable<Customer> GetAll()
        {
            return dbSet.Include(c => c.User).AsEnumerable();
        }

        public IEnumerable<Customer> GetAll(Expression<Func<Customer, bool>> filter)
        {
            return dbSet.Where(filter).Include(c => c.User).AsEnumerable();
        }
    }
}
