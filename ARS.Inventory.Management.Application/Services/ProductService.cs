using ARS.Inventory.Management.Domain.Interfaces;
using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Infrastructure.Interfaces;
using ARS.Inventory.Management.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ARS.Inventory.Management.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ProductRepository _productReposiyory;

        public ProductService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this._productReposiyory = new ProductRepository(unitOfWork);
        }

        public IEnumerable<Product> GetAll()
        {
            var result = _productReposiyory.GetAll();
            return result;
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return _productReposiyory.GetAllAsync();
        }

        public IEnumerable<Product> Get(Expression<Func<Product, bool>> filter)
        {
            var result = _productReposiyory.Get(filter);
            return result;
        }

        public Task<IEnumerable<Product>> GetAsync(Expression<Func<Product, bool>> filter)
        {
            return _productReposiyory.GetAsync(filter);
        }

        public Task<IEnumerable<Product>> GetPagedAsync(Expression<Func<Product, bool>> filter, int skip)
        {
            return _productReposiyory.GetPagedAsync(filter, skip);
        }

        public Task<int> GetCountAsync(Expression<Func<Product, bool>> filter)
        {
            return _productReposiyory.GetCountAsync(filter);
        }

        public Product GetById(int productId)
        {
            Product result = null;

            if (productId != null)
                result = _productReposiyory.GetById(c => c.Id == productId);

            return result;
        }

        public void Insert(Product product)
        {
            if (product != null)
                _productReposiyory.Insert(product);
        }

        public void Update(Product product)
        {
            if (product != null)
                _productReposiyory.Update(product);
        }

        public void Delete(int productId)
        {
            if (productId != null)
                _productReposiyory.Delete(x => x.Id == productId);
        }
    }
}
