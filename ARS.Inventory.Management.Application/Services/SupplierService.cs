using ARS.Inventory.Management.Domain.Interfaces;
using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Infrastructure.Interfaces;
using ARS.Inventory.Management.Infrastructure.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARS.Inventory.Management.Application.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SupplierRepository _supplierRepository;

        public SupplierService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._supplierRepository = new SupplierRepository(_unitOfWork);
        }

        public IEnumerable<Supplier> GetAll()
        {
            var result = _supplierRepository.GetAll();
            return result;
        }

        public Supplier GetById(int supplierId)
        {
            if (supplierId != default)
                return _supplierRepository.GetById(c => c.Id == supplierId);

            return null;
        }

        public void Insert(Supplier supplier)
        {
            if (supplier != null)
                _supplierRepository.Insert(supplier);
        }

        public async Task InsertAsync(Supplier supplier)
        {
            if (supplier != null)
                await _supplierRepository.InsertAsync(supplier);
        }

        public void Update(Supplier supplier)
        {
            if (supplier != null)
                _supplierRepository.Update(supplier);
        }

        public void Delete(int supplierId)
        {
            if (supplierId != default)
                _supplierRepository.Delete(x => x.Id == supplierId);
        }
    }
}
