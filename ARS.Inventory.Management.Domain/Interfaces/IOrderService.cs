using ARS.Inventory.Management.Domain.Models;

namespace ARS.Inventory.Management.Domain.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAll();
        IEnumerable<Order> GetAllConfirmedOrders();
        IEnumerable<Order> GetMyOrders(string userId);
        IEnumerable<Order> GetAllUnConfirmedOrders();
        Order GetById(int orderId);
        void Insert(Order order);
        void Update(Order order);
        void Delete(int orderId);
        void ConfirmOrder(int orderId);
        Task<Order> GetByIdAsync(int orderId);
    }
}
