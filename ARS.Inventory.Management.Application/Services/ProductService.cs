using ARS.Inventory.Management.Domain.Interfaces;
using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Infrastructure.Interfaces;
using ARS.Inventory.Management.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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
