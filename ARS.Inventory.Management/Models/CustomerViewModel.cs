using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ARS.Inventory.Management.Web.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [ValidateNever]
        public string CreatedBy { get; set; }
        [ValidateNever]
        public string CreatedByName { get; set; }
        public decimal Balance { get; set; }
        [ValidateNever]
        public string Comments { get; set; } = "";
    }
}
