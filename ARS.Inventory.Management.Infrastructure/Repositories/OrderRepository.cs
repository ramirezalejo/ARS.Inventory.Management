using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Infrastructure.Interfaces;
using ARS.Inventory.Management.Infrastructure.Repository.Context;

namespace ARS.Inventory.Management.Infrastructure.Repository.Repositories
{
    public class OrderRepository : BaseRepository<Order>
    {
        public OrderRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    }
}
