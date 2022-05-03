using System.Security.Claims;
using ARS.Inventory.Management.Domain.Interfaces;
using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Web.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ARS.Inventory.Management.Controllers.Api
{
    [Authorize]
    public class OrdersController : Controller
    {
        private IOrderService _order;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService order, IMapper mapper)
        {
            _order = order;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _order.GetAll().Select(x => new OrderViewModel
            {
                Id = x.Id,
                Product = _mapper.Map<Product, ProductsViewModel>(x.Product),
                UserId = x.UserId,
                OrderDate = x.OrderDate,
                ConfirmDate = x.ConfirmDate,
                ConfirmStatus = x.ConfirmStatus
            });
            return Ok(result);
        }

        [Route("api/Orders/ConfirmedOrders")]
        [HttpGet]
        public IActionResult GetAllConfirmedOrders()
        {
            var result = _order.GetAllConfirmedOrders().Select(x => new OrderViewModel
            {
                Id = x.Id,
                Product = _mapper.Map<Product, ProductsViewModel>(x.Product),
                UserId = x.UserId,
                OrderDate = x.OrderDate,
                ConfirmDate = x.ConfirmDate,
                ConfirmStatus = x.ConfirmStatus
            });
            return Ok(result);
        }
        [Route("api/Orders/GetMyOrders")]
        [HttpGet]
        public IActionResult GetMyOrders()
        {
            var UserId = User.Identity.Name;
            var result = _order.GetMyOrders(UserId)
                .Select(x => new OrderViewModel
                {
                    Id = x.Id,
                    Product = _mapper.Map<Product, ProductsViewModel>(x.Product),
                    UserId = x.UserId,
                    OrderDate = x.OrderDate,
                    ConfirmDate = x.ConfirmDate,
                    ConfirmStatus = x.ConfirmStatus,
                    CustomerId = x.CustomerId,
                    CustomerName = x.Customer.Name,
                    ShippingAddress = x.ShippingAddress ?? x.Customer.Address

                }).OrderByDescending(x => x.OrderDate);

            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var result = _order.GetById(id);
            if (result != null)
            {
                OrderViewModel vm = new OrderViewModel
                {
                    Id = result.Id,
                    OrderDate = result.OrderDate,
                    Product = _mapper.Map<Product, ProductsViewModel>(result.Product),
                    CustomerId= result.CustomerId,
                    ConfirmDate = result.ConfirmDate,
                    ConfirmStatus = result.ConfirmStatus,
                    UserId = result.UserId
                };
                return Ok(vm);
            }
            return Ok("Item Not Found!");
        }
        [HttpPost]
        public IActionResult Insert(OrderViewModel model)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Order order = new Order
            {
                ProductId = model.ProductId,
                UserId = user,
                CustomerId = model.CustomerId,
                ShippingAddress = model.ShippingAddress
            };
            _order.Insert(order);
            return Ok(order);
        }

        [HttpPut]
        public IActionResult Update(OrderViewModel model)
        {
            Order order = new Order
            {
                Id = model.Id,
                ProductId = model.ProductId,
                UserId = model.UserId,
                CustomerId = model.CustomerId,
                ShippingAddress = model.ShippingAddress
            };
            _order.Update(order);
            return Ok(order);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _order.Delete(id);
            return Ok("Deleted Successfully !");
        }

        [Route("api/Orders/ConfirmOrder")]
        public IActionResult ConfirmOrder(int id)
        {
            _order.ConfirmOrder(id);
            return Ok("Order Confirmend !");
        }
    }
}
