using ARS.Inventory.Management.Domain.Models;
using System.Linq.Expressions;

namespace ARS.Inventory.Management.Domain.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Task<IEnumerable<Product>> GetAllAsync();
        IEnumerable<Product> Get(Expression<Func<Product, bool>> filter);
        Task<IEnumerable<Product>> GetAsync(Expression<Func<Product, bool>> filter);
        Product GetById(int productId);
        void Insert(Product product);
        void Update(Product product);
        void Delete(int productId);
    }
}
