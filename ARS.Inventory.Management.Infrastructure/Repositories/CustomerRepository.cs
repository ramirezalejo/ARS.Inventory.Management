using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Infrastructure.Interfaces;
using ARS.Inventory.Management.Infrastructure.Repository.Context;

namespace ARS.Inventory.Management.Infrastructure.Repository
{
    public class CustomerRepository : BaseRepository<Customer>
    {
        public CustomerRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
