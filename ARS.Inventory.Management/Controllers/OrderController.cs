using ARS.Inventory.Management.Domain.Interfaces;
using ARS.Inventory.Management.Models;

namespace ARS.Inventory.Management.Controllers
{
    public class OrderController : Microsoft.AspNetCore.Mvc.Controller
    {
        private IOrderService _order;

        public OrderController(IOrderService orders)
        {
            this._order = orders;
        }
        // GET: Orders
        public Microsoft.AspNetCore.Mvc.ActionResult ListOrder()
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

        public Microsoft.AspNetCore.Mvc.ActionResult ConfirmedListOrder()
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