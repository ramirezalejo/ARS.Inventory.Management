namespace ARS.Inventory.Management.Web.Models
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal SellingPrice { get; set; }

        public virtual decimal Price 
        { 
            get
            {
                return SellingPrice * Quantity;
            }
        }



        public ProductsViewModel Product { get; set; }
        public CategoryViewModel Category { get; set; }
    }
}
