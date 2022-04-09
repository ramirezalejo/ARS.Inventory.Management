using ARS.Inventory.Management.Domain.Interfaces;
using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Infrastructure.Interfaces;
using ARS.Inventory.Management.Infrastructure.Repositories;

namespace ARS.Inventory.Management.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CustomerRepository _customerRepository;

        public CustomerService(IUnitOfWork unitOfWork, CustomerRepository customerRepository)
        {
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
        }

        public IEnumerable<Customer> GetAllPaginated(int page = 1, int pageSize = 100)
        {
            var result = _customerRepository.GetAllPaginated(page -1, pageSize);
            return result;
        }
        public IEnumerable<Customer> GetCustomersByName(string name)
        {
            var result = _customerRepository.GetAll(x => x.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase));
            return result;
        }

        public IEnumerable<Customer> GetByUserId(string userId)
        {
            if (userId != null)
                return _customerRepository.GetAll(c => c.CreatedBy == userId);

            return null;
        }

        public Customer GetById(int customerId)
        {
            return _customerRepository.GetById(c => c.Id == customerId);
        }

        public void Insert(Customer customer)
        {
            if (customer != null)
                _customerRepository.Insert(customer);
        }

        public async Task InsertAsync(Customer customer)
        {
            if (customer != null)
                await _customerRepository.InsertAsync(customer);
        }

        public void Update(Customer customer)
        {
            if (customer != null)
                _customerRepository.Update(customer);
        }

        public void Delete(int cutomerId)
        {
            _customerRepository.Delete(c => c.Id == cutomerId);
        }
    }
}
