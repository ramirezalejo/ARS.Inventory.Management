using ARS.Inventory.Management.Domain.Interfaces;
using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Web.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ARS.Inventory.Management.Controllers
{
    [Authorize(Roles = "Admin,Seller")]
    public class OrderController : Controller
    {
        private IOrderService _order;
        private readonly IProductService _product;
        private readonly ICategoryService _category;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orders, 
            IProductService product,
            ICategoryService category,
            IMapper mapper )
        {
            this._order = orders;
            _product = product;
            _category = category;
            _mapper = mapper;
        }
        // GET: Orders
        public ActionResult ListOrder()
        {
            var result = _order.GetLatestUnConfirmedOrders()
            .Select(m => _mapper.Map<Order, OrderViewModel>(m)).OrderByDescending(x => x.OrderDate);

            return View(result);
        }

        public ActionResult ConfirmedListOrder()
        {
            var result = _order.GetLatestConfirmedOrders()
            .Select(m => _mapper.Map<Order, OrderViewModel>(m)).OrderByDescending(x=>x.ConfirmDate);

            return View(result);
        }

        public IActionResult EditOrder(int id)
        {
            var category = _category.GetAll().ToList();
            ViewBag.Category = new SelectList(category, "Id", "Name");
            if (id > 0)
            {
                var order = _order.GetById(id);
                if (order != null)
                {
                    return View("EditOrder", _mapper.Map<Order, OrderViewModel>(order));
                }
            }
            return View("EditOrder");
        }

    }
}