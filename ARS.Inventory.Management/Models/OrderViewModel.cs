using ARS.Inventory.Management.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace ARS.Inventory.Management.Web.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ConfirmDate { get; set; }
        public bool ConfirmStatus { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ShippingAddress { get; set; }

        public  Product Product { get; set; }
        public ApplicationUser User { get; set; }
        public IdentityRole UserRole { get; set; }
    }
}