using ARS.Inventory.Management.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace ARS.Inventory.Management.Web.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Today;
        public DateTime ConfirmDate { get; set; }
        public bool ConfirmStatus { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ShippingAddress { get; set; }

        public List<OrderDetailViewModel> OrderDetails { get; set; }
        public ApplicationUser User { get; set; }
        public IdentityRole UserRole { get; set; }
    }
}