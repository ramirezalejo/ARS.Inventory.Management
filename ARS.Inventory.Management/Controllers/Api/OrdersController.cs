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

        [Route("api/Orders/ConfirmedOrders")]
        [HttpGet]
        public IActionResult GetAllConfirmedOrders()
        {
            var result = _order.GetLatestConfirmedOrders().Select(x => new OrderViewModel
            {
                Id = x.Id,
                OrderDetails = _mapper.Map<List<OrderDetail>, List<OrderDetailViewModel>>(x.OrderDetails),
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
                    OrderDetails = _mapper.Map<List<OrderDetail>, List<OrderDetailViewModel>>(x.OrderDetails),
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
                    OrderDetails = _mapper.Map<List<OrderDetail>, List<OrderDetailViewModel>>(result.OrderDetails),
                    CustomerId = result.CustomerId,
                    ConfirmDate = result.ConfirmDate,
                    ConfirmStatus = result.ConfirmStatus,
                    UserId = result.UserId
                };
                return Ok(vm);
            }
            return Ok("Item Not Found!");
        }
        [HttpPost]
        public async Task<IActionResult> Insert(OrderViewModel model)
        {
            //TODO: Validate product existence

            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Order order = new Order
            {

                OrderDetails = _mapper.Map<List<OrderDetailViewModel>, List<OrderDetail>>(model.OrderDetails),
                UserId = user,
                CustomerId = model.CustomerId,
                ShippingAddress = model.ShippingAddress
            };
            await _order.InsertAsync(order);
            return Ok(order);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(OrderViewModel model)
        {
            Order order = new Order
            {
                Id = model.Id,
                OrderDetails = _mapper.Map<List<OrderDetailViewModel>, List<OrderDetail>>(model.OrderDetails),
                UserId = model.UserId,
                CustomerId = model.CustomerId,
                ShippingAddress = model.ShippingAddress
            };
            await _order.UpdateAsync(order);
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
