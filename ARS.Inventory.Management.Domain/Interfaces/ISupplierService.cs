using ARS.Inventory.Management.Domain.Models;

namespace ARS.Inventory.Management.Domain.Interfaces
{
    public interface ISupplierService
    {
        IEnumerable<Supplier> GetAll();
        Supplier GetById(int supplierId);
        void Insert(Supplier supplier);
        void Update(Supplier supplier);
        void Delete(int supplierId);
    }
}
