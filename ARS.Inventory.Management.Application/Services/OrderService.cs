using ARS.Inventory.Management.Domain.Interfaces;
using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Infrastructure.Interfaces;
using ARS.Inventory.Management.Infrastructure.Repositories;
using ARS.Inventory.Management.Infrastructure.Repository.Repositories;
using Microsoft.Extensions.Localization;
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
        private readonly OrderDetailRepository _orderDetailRepository;
        private readonly IProductService _productService;
        private readonly IStringLocalizer<OrderService> _localizer;

        public OrderService(IUnitOfWork _unitOfWork, 
            IProductService productService,
            IStringLocalizer<OrderService> localizer)
        {
            unitOfWork = _unitOfWork;
            _productService = productService;
            _localizer = localizer;
            _orderRepository = new OrderRepository(unitOfWork);
            _orderDetailRepository = new OrderDetailRepository(unitOfWork);
        }
        public IEnumerable<Order> GetLatestOrders()
        {
            var result = _orderRepository.GetAll().OrderByDescending(x => x.OrderDate).Take(10);
            return result;
        }
        public IEnumerable<Order> GetCurrentMonthOrders()
        {
            var filterDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);
            var result = _orderRepository.Get(x => x.ConfirmDate > filterDate).OrderByDescending(x => x.OrderDate);
            return result;
        }
        public IEnumerable<Order> GetLatestConfirmedOrders()
        {
            var result = _orderRepository.Get(x => x.ConfirmStatus == true).OrderByDescending(x => x.ConfirmDate).Take(10);
            return result;
        }

        public IEnumerable<Order> GetMyOrders(string userId)
        {
            var result = _orderRepository.Get(x => x.UserId == userId);
            return result;
        }

        public IEnumerable<Order> GetLatestUnConfirmedOrders()
        {
            var result = _orderRepository.Get(x => x.ConfirmStatus == false).OrderByDescending(x => x.OrderDate).Take(10);
            return result;
        }

        public Order GetById(int orderId)
        {
            Order result = null;
            if (orderId != null)
                result = _orderRepository.GetById(x => x.Id == orderId);

            return result;
        }

        public Task<Order> GetByIdAsync(int orderId)
        {
            return _orderRepository.GetByIdAsync(x => x.Id == orderId);
        }

        public void Insert(Order order)
        {
            throw new NotImplementedException();

            //if (order != null)
            //{

            //    order.OrderDate = DateTime.Now;
            //    order.ConfirmStatus = false;
            //    order.ConfirmDate = DateTime.Now;
            //    var products = _productService.GetById(order.ProductId);
            //    product.Quantity--;
            //    _orderRepository.Insert(order);
            //    _productService.Update(product);
            //}
        }

        public async Task InsertAsync(Order order)
        {
            if (order != null)
            {
                var tasks = new List<Task>();
                //order.OrderDate = DateTime.Now;
                order.ConfirmStatus = false;
                //order.ConfirmDate = DateTime.Now;
                //foreach (OrderDetail orderDetail in order.OrderDetails)
                //{
                //    var product = _productService.GetById(orderDetail.ProductId);
                //    product.Quantity -= orderDetail.Quantity;
                //    tasks.Add(_productService.UpdateAsync(product));

                //}
                tasks.Add(_orderDetailRepository.InsertAllAsync(order.OrderDetails));
                tasks.Add(_orderRepository.InsertAsync(order));
                await Task.WhenAll(tasks);
            }
        }

        public void Update(Order order)
        {
            throw new NotImplementedException();
            //if (order != null)
            //{
            //    order.OrderDate = DateTime.Now;
            //    order.ConfirmDate = DateTime.Now;
            //    order.ConfirmStatus = false;
            //    _orderRepository.Update(order);
            //}
        }

        public async Task UpdateAsync(Order order)
        {
            if (order != null)
            {
                var tasks = new List<Task>();
                var previousOrderData = await _orderRepository.GetByIdAsync(x => x.Id == order.Id);
                //order.OrderDate = DateTime.Now;
                order.ConfirmStatus = false;
                //order.ConfirmDate = DateTime.Now;
                foreach (OrderDetail orderDetail in order.OrderDetails)
                {
                    var previousOrderDataDetail = previousOrderData.OrderDetails.FirstOrDefault(x => x.ProductId == orderDetail.ProductId);
                    //var product = _productService.GetById(orderDetail.ProductId);
                    if (previousOrderDataDetail is null)
                    {
                        tasks.Add(_orderDetailRepository.InsertAsync(orderDetail));
                        //product.Quantity -= orderDetail.Quantity;
                        //tasks.Add(_productService.UpdateAsync(product));
                    }
                    else if (previousOrderDataDetail.Quantity != orderDetail.Quantity)
                    {
                        tasks.Add(_orderDetailRepository.UpdateAsync(orderDetail));
                        //product.Quantity += previousOrderDataDetail.Quantity - orderDetail.Quantity;
                        //tasks.Add(_productService.UpdateAsync(product));
                    }
                }
                var orphanDetails = previousOrderData.OrderDetails.Where(x => !order.OrderDetails.Any(y => y.ProductId == x.ProductId));
                if (orphanDetails.Any())
                {
                    tasks.Add(_orderDetailRepository.DeleteAsync(orphanDetails));
                    //foreach(OrderDetail orderDetail in orphanDetails)
                    //{
                    //    var product = _productService.GetById(orderDetail.ProductId);
                    //    product.Quantity += orderDetail.Quantity;
                    //}
                }
                    
                tasks.Add(_orderRepository.UpdateAsync(order));
                await Task.WhenAll(tasks);
            }
        }

        public void Delete(int orderId)
        {
            if (orderId != default)
                _orderRepository.Delete(x => x.Id == orderId);
        }

        public async Task ConfirmOrder(int orderId)
        {
            await UpdateConfirmation(orderId, true);
        }

        public async Task UnConfirmOrder(int orderId)
        {
            await UpdateConfirmation(orderId, false);
        }

        public async Task UpdateConfirmation(int orderId, bool confirm)
        {
            if (orderId != default)
            {
                var tasks = new List<Task>();
                var errors = new List<string>();
                var result = _orderRepository.GetById(x => x.Id == orderId);
                result.ConfirmStatus = true;
                result.ConfirmDate = DateTime.Now;
                foreach (OrderDetail orderDetail in result.OrderDetails)
                {
                    var product = _productService.GetById(orderDetail.ProductId);

                    if (confirm)
                        product.Quantity -= orderDetail.Quantity;
                    else
                        product.Quantity += orderDetail.Quantity;

                    if (product.Quantity >= 0)
                        tasks.Add(_productService.UpdateAsync(product));
                    else
                        errors.Add(string.Format(_localizer["Available_quantity_exceeded"], product.Quantity, product.Name));
                }

                if (errors.Any())
                    throw new ArgumentException(message: String.Join("/r/n", errors));

                await Task.WhenAll(tasks);
                await _orderRepository.UpdateAsync(result);
            }
        }

    }
}
