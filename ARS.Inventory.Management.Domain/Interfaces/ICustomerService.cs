using ARS.Inventory.Management.Domain.Models;

namespace ARS.Inventory.Management.Domain.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAllPaginated(int page = 1, int pageSize = 100);
        IEnumerable<Customer> GetByUserId(string userId);
        IEnumerable<Customer> GetCustomersByName(string name);
        Customer GetById(int customerId);
        void Insert(Customer customer);
        Task InsertAsync(Customer customer);
        void Update(Customer customer);
        void Delete(int cutomerId);
    }
}
