using ARS.Inventory.Management.Domain.Models;

namespace ARS.Inventory.Management.Domain.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
        Category GetById(int categoryId);
        void Insert(Category category);
        void Update(Category category);
        void Delete(int categoryId);
    }
}
