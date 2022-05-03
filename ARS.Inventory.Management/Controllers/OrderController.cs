using ARS.Inventory.Management.Domain.Interfaces;
using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Web.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ARS.Inventory.Management.Controllers
{
    [Authorize(Roles = "Admin,Seller")]
    public class OrderController : Controller
    {
        private IOrderService _order;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orders, IMapper mapper )
        {
            this._order = orders;
            _mapper = mapper;
        }
        // GET: Orders
        public ActionResult ListOrder()
        {
            var result = _order.GetAllUnConfirmedOrders()
            .Select(m => new OrderViewModel
            {
                Id = m.Id,
                Product = _mapper.Map<Product, ProductsViewModel>(m.Product),
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
                Product = _mapper.Map<Product, ProductsViewModel>(m.Product),
                User = m.User,
                OrderDate = m.OrderDate,
                ConfirmStatus = m.ConfirmStatus
            }).OrderByDescending(x=>x.OrderDate);

            return View(result);
        }

        public IActionResult EditOrder()
        {
            return View();
        }

    }
}