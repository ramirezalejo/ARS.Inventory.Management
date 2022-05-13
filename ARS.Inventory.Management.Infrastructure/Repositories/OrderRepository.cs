using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Infrastructure.Interfaces;
using ARS.Inventory.Management.Infrastructure.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ARS.Inventory.Management.Infrastructure.Repository.Repositories
{
    public class OrderRepository : BaseRepository<Order>
    {
        public OrderRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Order> GetByIdAsync(Expression<Func<Order, bool>> filter)
        {
            var dbResult = await dbSet.Include(c => c.OrderDetails).FirstAsync(filter);
            return dbResult;
        }

        public override IEnumerable<Order> GetAll()
        {
            return dbSet.Include(c => c.OrderDetails).AsEnumerable();
        }

        public override IEnumerable<Order> Get(Expression<Func<Order, bool>> filter)
        {
            return dbSet.Include(c => c.OrderDetails).Where(filter).AsEnumerable();
        }
       public override Order GetById(Expression<Func<Order, bool>> filter)
        {
            return dbSet.Where(filter).Include(c => c.OrderDetails).SingleOrDefault();
        }
    }
}
