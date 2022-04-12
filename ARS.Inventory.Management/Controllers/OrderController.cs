using ARS.Inventory.Management.Domain.Interfaces;
using ARS.Inventory.Management.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ARS.Inventory.Management.Controllers
{
    [Authorize(Roles = "Admin,Seller")]
    public class OrderController : Controller
    {
        private IOrderService _order;

        public OrderController(IOrderService orders)
        {
            this._order = orders;
        }
        // GET: Orders
        public ActionResult ListOrder()
        {
            var result = _order.GetAllUnConfirmedOrders()
            .Select(m => new OrderViewModel
            {
                Id = m.Id,
                Product = m.Product,
                User = m.User,
                OrderDate = m.OrderDate,
                ConfirmStatus = m.ConfirmStatus
            }).OrderByDescending(x=>x.OrderDate);

            return View(result);
        }

        public ActionResult ConfirmedListOrder()
        {
            var result = _order.GetAllConfirmedOrders()
            .Select(m => new OrderViewModel
            {
                Id = m.Id,
                Product = m.Product,
                User = m.User,
                OrderDate = m.OrderDate,
                ConfirmStatus = m.ConfirmStatus
            }).OrderByDescending(x=>x.OrderDate);

            return View(result);
        }

    }
}