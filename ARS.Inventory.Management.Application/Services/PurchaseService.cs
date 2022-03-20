using ARS.Inventory.Management.Domain.Interfaces;
using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Infrastructure.Interfaces;
using ARS.Inventory.Management.Infrastructure.Repository;
using ARS.Inventory.Management.Infrastructure.Repository.Repositories;

namespace ARS.Inventory.Management.Application.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PurchaseRepository _purchaseRepository;
        private readonly ProductRepository _productRepository;

        public PurchaseService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._purchaseRepository = new PurchaseRepository(_unitOfWork);
            this._productRepository = new ProductRepository(_unitOfWork);
        }

        public IEnumerable<Purchase> GetAll()
        {
            var result = _purchaseRepository.GetAll();
            return result;
        }

        public Purchase GetById(int purchaseId)
        {
            Purchase result = null;
            if (purchaseId != null)
                result = _purchaseRepository.GetById(x => x.Id == purchaseId);
            return result;
        }

        public void Insert(Purchase purchase)
        {
            if (purchase != null)
            {
                purchase.CreatedTime = DateTime.Now;
                _purchaseRepository.Insert(purchase);
            }
        }

        public void Update(Purchase purchase)
        {
            if (purchase != null)
            {
                _purchaseRepository.Update(purchase);
            }
        }

        public void Delete(int purchaseId)
        {
            if (purchaseId != null)
            {
                var purchase = _purchaseRepository.GetById(x => x.Id == purchaseId);
                _purchaseRepository.Delete(x => x.Id == purchaseId);

                var product = _productRepository.GetById(p => p.Id == purchase.ProductId);
                product.Quantity -= purchase.Quantity;
                _productRepository.Update(product);
            }
        }

        public void ConfirmPurchase(int purchaseId)
        {
            if (purchaseId != null)
            {
                var purchase = _purchaseRepository.GetById(p => p.Id == purchaseId);
                purchase.Confirmation = true;
                purchase.ConfirmationTime = DateTime.Now;
                _purchaseRepository.Update(purchase);

                var product = _productRepository.GetById(p => p.Id == purchase.ProductId);
                product.Quantity += purchase.Quantity;
                _productRepository.Update(product);
            }
        }

        public void UnconfirmPurchase(int purchaseId)
        {
            if (purchaseId != null)
            {
                var purchase = _purchaseRepository.GetById(p => p.Id == purchaseId);
                purchase.Confirmation = false;
                _purchaseRepository.Update(purchase);

                var product = _productRepository.GetById(p => p.Id == purchase.ProductId);
                product.Quantity -= purchase.Quantity;
                _productRepository.Update(product);
            }
        }
    }
}
