using ARS.Inventory.Management.Domain.Models;

namespace ARS.Inventory.Management.Domain.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<Order> GetLatestOrders();
        IEnumerable<Order> GetCurrentMonthOrders();
        IEnumerable<Order> GetLatestConfirmedOrders();
        IEnumerable<Order> GetMyOrders(string userId);
        IEnumerable<Order> GetLatestUnConfirmedOrders();
        Order GetById(int orderId);
        void Insert(Order order);
        Task InsertAsync(Order order);
        void Update(Order order);
        Task UpdateAsync(Order order);
        void Delete(int orderId);
        Task ConfirmOrder(int orderId);
        Task UnConfirmOrder(int orderId);
        Task<Order> GetByIdAsync(int orderId);
    }
}
