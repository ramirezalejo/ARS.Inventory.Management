using ARS.Inventory.Management.Domain.Interfaces;
using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Infrastructure.Interfaces;
using ARS.Inventory.Management.Infrastructure.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARS.Inventory.Management.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly OrderRepository _orderRepository;
        private readonly IProductService _productService;

        public OrderService(IUnitOfWork _unitOfWork, IProductService productService)
        {
            unitOfWork = _unitOfWork;
            _productService = productService;
            _orderRepository = new OrderRepository(unitOfWork);
        }
        public IEnumerable<Order> GetAll()
        {
            var result = _orderRepository.GetAll();
            return result;
        }
        public IEnumerable<Order> GetAllConfirmedOrders()
        {
            var result = _orderRepository.GetAll(x => x.ConfirmStatus == true);
            return result;
        }

        public IEnumerable<Order> GetMyOrders(string userId)
        {
            var result = _orderRepository.GetAll(x => x.UserId == userId);
            return result;
        }

        public IEnumerable<Order> GetAllUnConfirmedOrders()
        {
            var result = _orderRepository.GetAll(x => x.ConfirmStatus == false);
            return result;
        }

        public Order GetById(int orderId)
        {
            Order result = null;
            if (orderId != null)
                result = _orderRepository.GetById(x => x.Id == orderId);

            return result;
        }

        public void Insert(Order order)
        {

            if (order != null)
            {

                order.OrderDate = DateTime.Now;
                order.ConfirmStatus = false;
                order.ConfirmDate = DateTime.Now;
                var product = _productService.GetById(order.ProductId);
                product.Quantity--;
                _orderRepository.Insert(order);
                _productService.Update(product);
            }
        }

        public void Update(Order order)
        {
            if (order != null)
            {
                order.OrderDate = DateTime.Now;
                order.ConfirmDate = DateTime.Now;
                order.ConfirmStatus = false;
                _orderRepository.Update(order);
            }
        }

        public void Delete(int orderId)
        {
            if (orderId != null)
                _orderRepository.Delete(x => x.Id == orderId);
        }

        public void ConfirmOrder(int orderId)
        {
            if (orderId != null)
            {
                var result = _orderRepository.GetById(x => x.Id == orderId);
                result.ConfirmStatus = true;
                result.ConfirmDate = DateTime.Now;
                _orderRepository.Update(result);
            }
        }
    }
}
