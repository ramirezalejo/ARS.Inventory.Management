using ARS.Inventory.Management.Domain.Interfaces;
using ARS.Inventory.Management.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ARS.Inventory.Management.Controllers
{


    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        ISupplierService _supplier;
        IProductService _product;
        IOrderService _order;
        IUserService _user;
        IPurchaseService _purchase;

        public HomeController(ISupplierService supplier, IProductService product, IOrderService order, IUserService user, IPurchaseService purchase)
        {
            this._supplier = supplier;
            this._product = product;
            this._order = order;
            this._user = user;
            this._purchase = purchase;
        }

        public Microsoft.AspNetCore.Mvc.ActionResult Index()
        {
            var vm = new HomeViewModel();

            //vm.TotalSuppliers = _supplier.GetAll().Count<Supplier>();
            //vm.TotalOrders = _order.GetAll().Count<Orders>();
            //vm.TotalProducts = _product.GetAll().Count<Product>();

            if(User.IsInRole("Admin"))
            {
                #region Total Product Cost
                var result = _product.GetAll();
                foreach (var item in result)
                {
                    vm.TotalProductValues += (item.PurchasingPrice * item.Quantity);
                }
                #endregion

                #region Total Purchase
                var purchase = _purchase.GetAll();
                foreach (var item in purchase)
                {
                    vm.TotalPurchase += (item.Quantity * item.Product.PurchasingPrice);
                }
                #endregion

                #region Total Revenue 
                var order = _order.GetAll();

                foreach (var item in order)
                {
                    vm.TotalOrders += item.Product.SellingPrice;
                }
                #endregion

                #region Profit

                foreach (var item in order)
                {
                    vm.Profit += (item.Product.SellingPrice - item.Product.PurchasingPrice);
                }

                #endregion
            }


           

            #region Top Seller
            vm.TopSeller = _order.GetAll()
                .GroupBy(i => new { i.ProductId, i.Product.Name })
                .Select(i => new TopSeller
                {
                    ProductName = i.Key.Name,
                    ProductCount = i.Count()
                }).OrderByDescending(x => x.ProductCount).Take(5);
            #endregion

            #region Most Sold Categories

            vm.MostSoldCategories = _order.GetAll()
               .GroupBy(i => new { i.Product.Category.Id, i.Product.Category.Name })
               .Select(i => new MostSoldCategories
               {
                   CategoryName = i.Key.Name,
                   CategoryCount = i.Count()
               }).OrderByDescending(x => x.CategoryCount).Take(5);
            #endregion

            #region Most Profitable Products
            vm.MostProfitableProducts = _order.GetAll()
               .GroupBy(i => new { i.Product.Id, i.Product.Name, i.Product.SellingPrice, i.Product.PurchasingPrice })
               .Select(i => new MostProfitableProducts
               {
                   ProductName = i.Key.Name,
                   SellingPrice = i.Key.SellingPrice,
                   PurchasingPrice = i.Key.PurchasingPrice,
                   Profit = (i.Key.SellingPrice  - i.Key.PurchasingPrice)
               }).OrderByDescending(x => x.Profit).Take(5);




            #endregion

            vm.LatesOrders = _order.GetAll().OrderByDescending(x => x.OrderDate).Take(5);
            vm.LatesPurchases = _purchase.GetAll().OrderByDescending(x => x.CreatedTime).Take(5);

            vm.User = _user.GetAll().OrderByDescending(x => x.RegisteredDate).Take(5);

            return View(vm);
        }


    }
}
