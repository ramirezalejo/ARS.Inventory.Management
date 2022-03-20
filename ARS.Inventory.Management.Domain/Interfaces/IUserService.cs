using ARS.Inventory.Management.Domain.Models;

namespace ARS.Inventory.Management.Domain.Interfaces
{
    public interface IUserService
    {
        void Delete(string userId);
        IEnumerable<ApplicationUser> GetAll();
        ApplicationUser GetById(string id);
        void Insert(ApplicationUser category);
        void Update(ApplicationUser category);
    }
}
