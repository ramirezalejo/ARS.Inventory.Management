using ARS.Inventory.Management.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace ARS.Inventory.Management.Web.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int ProductId { get { return Product.Id; } } 
        public string ProductName { get { return Product.Name; } }
        public string ProductCategory { get { return Product.CategoryName; } }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Today;
        public DateTime ConfirmDate { get; set; }
        public bool ConfirmStatus { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ShippingAddress { get; set; }

        public  ProductsViewModel Product { get; set; }
        public CategoryViewModel Category { get; set; } = new CategoryViewModel();
        public ApplicationUser User { get; set; }
        public IdentityRole UserRole { get; set; }
    }
}