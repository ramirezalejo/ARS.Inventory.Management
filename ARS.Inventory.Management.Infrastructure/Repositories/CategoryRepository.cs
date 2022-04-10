using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Infrastructure.Interfaces;
using ARS.Inventory.Management.Infrastructure.Repository.Context;

namespace ARS.Inventory.Management.Infrastructure.Repository
{
    public class CategoryRepository : BaseRepository<Category>
    {
        public CategoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        
    }
}
