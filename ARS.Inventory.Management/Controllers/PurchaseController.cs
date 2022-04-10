using ARS.Inventory.Management.Domain.Interfaces;
using ARS.Inventory.Management.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace ARS.Inventory.Management.Controllers
{
    public class PurchaseController : Controller
    {
        private IProductService _product;
        private IPurchaseService _purchase;

        public PurchaseController(IProductService product, IPurchaseService purchase)
        {
            this._product = product;
            this._purchase = purchase;
        }

        public ActionResult ListPurchase()
        {
            var result = _purchase.GetAll()
                .Where(p=> p.Confirmation == false)
                .Select(m => new PurchaseViewModel
                {
                    Id = m.Id,
                    Product = m.Product,
                    User = m.User,
                    Quantity = m.Quantity,
                    CreatedTime = m.CreatedTime,
                    DeliveryTime = m.DeliveryTime,
                    Description = m.Description,
                    Confirmation = m.Confirmation
                }).OrderByDescending(x => x.CreatedTime);
            return View(result);
        }

        public ActionResult ConfirmedPurchase()
        {
            var result = _purchase.GetAll()
                .Where(p => p.Confirmation == true)
                .Select(p => new PurchaseViewModel
                {
                    Id = p.Id,
                    Product = p.Product,
                    Quantity = p.Quantity,
                    CreatedTime = p.CreatedTime,
                    DeliveryTime = p.DeliveryTime,
                    ConfirmationTime = p.ConfirmationTime
                }).OrderByDescending(x => x.ConfirmationTime);

            return View(result);
        }

        public ActionResult AddPurchase()
        {
            var product = _product.GetAll();
            ViewBag.Products = new SelectList(product, "Id", "Name");
            return View();
        }


        public ActionResult EditPurchase(int id)
        {
            if (id > 0)
            {
                var purchase = _purchase.GetById(id);

                if (purchase != null)
                {
                    var vm = new PurchaseViewModel
                    {
                        Id = purchase.Id,
                        ProductId = purchase.ProductId,
                        Quantity = purchase.Quantity,
                        DeliveryTime = purchase.DeliveryTime,
                        Description = purchase.Description
                    };

                    var product = _product.GetAll();
                    ViewBag.Products = new SelectList(product, "Id", "Name");

                    return View("AddPurchase", vm);
                }
            }

            return View("AddPurchase");
        }
    }
}