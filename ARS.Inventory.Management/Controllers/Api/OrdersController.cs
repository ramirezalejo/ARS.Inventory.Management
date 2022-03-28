using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using ARS.Inventory.Management.Domain.Interfaces;
using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ARS.Inventory.Management.Controllers.Api
{
    [Authorize]
    public class OrdersController : Controller
    {
        private IOrderService _order;

        public OrdersController(IOrderService order)
        {
            this._order = order;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _order.GetAll().Select(x => new OrderViewModel
            {
                Id = x.Id,
                ProductId = x.ProductId,
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
                ProductId = x.ProductId,
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
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    ProductCategory = x.Product.Category.Name,
                    UserId = x.UserId,
                    OrderDate = x.OrderDate,
                    ConfirmDate = x.ConfirmDate,
                    ConfirmStatus = x.ConfirmStatus,
                    ShippingAddress = x.ShippingAddress

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
                    ProductId = result.ProductId,
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
