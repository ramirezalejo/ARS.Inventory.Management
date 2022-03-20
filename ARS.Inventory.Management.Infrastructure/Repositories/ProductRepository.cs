using ARS.Inventory.Management.Infrastructure.Repository.Context;
using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Infrastructure.Interfaces;

namespace ARS.Inventory.Management.Infrastructure.Repository
{
    public class ProductRepository : BaseRepository<Product>
    {
        public ProductRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
