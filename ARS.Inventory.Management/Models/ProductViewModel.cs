using ARS.Inventory.Management.Domain.Models;

namespace ARS.Inventory.Management.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal PurchasingPrice { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
        public string CategoryName { get; set; }


        public Category Category { get; set; }
        public Supplier Supplier { get; set; }
    }
}