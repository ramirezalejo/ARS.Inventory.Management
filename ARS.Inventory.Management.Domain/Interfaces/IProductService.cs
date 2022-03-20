using ARS.Inventory.Management.Domain.Models;

namespace ARS.Inventory.Management.Domain.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product GetById(int productId);
        void Insert(Product product);
        void Update(Product product);
        void Delete(int productId);
    }
}
