using ARS.Inventory.Management.Domain.Models;

namespace ARS.Inventory.Management.Web.Models
{
    public class PurchaseViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime DeliveryTime { get; set; }
        public string Description { get; set; }
        public bool Confirmation { get; set; }
        public DateTime ConfirmationTime { get; set; }

        public Product Product { get; set; }
        public ApplicationUser User { get; set; }

    }
}