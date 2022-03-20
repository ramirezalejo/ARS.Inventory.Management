using ARS.Inventory.Management.Domain.Models;

namespace ARS.Inventory.Management.Domain.Interfaces
{
    public interface IPurchaseService
    {
        IEnumerable<Purchase> GetAll();
        Purchase GetById(int purchaseId);
        void Insert(Purchase purchase);
        void Update(Purchase purchase);
        void Delete(int purchaseId);
        void ConfirmPurchase(int purchaseId);
        void UnconfirmPurchase(int purchaseId);
    }
}
