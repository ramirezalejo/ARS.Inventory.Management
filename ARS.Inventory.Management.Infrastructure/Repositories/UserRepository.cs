using ARS.Inventory.Management.Infrastructure.Repository.Context;
using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Infrastructure.Interfaces;

namespace ARS.Inventory.Management.Infrastructure.Repository.Repositories
{
    public class UserRepository : BaseRepository<ApplicationUser>
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
