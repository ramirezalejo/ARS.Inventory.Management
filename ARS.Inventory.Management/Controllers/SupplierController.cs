using ARS.Inventory.Management.Domain.Interfaces;
using ARS.Inventory.Management.Web.Models;

namespace ARS.Inventory.Management.Controllers
{
    public class SupplierController : Microsoft.AspNetCore.Mvc.Controller
    {
        ISupplierService _supplier;
        IOrderService _order;
        IProductService _product;


        public SupplierController(ISupplierService supplier)
        {
            this._supplier = supplier;
        }
        // GET: Supplier
        public Microsoft.AspNetCore.Mvc.ActionResult ListSupplier()
        {
            var result = _supplier.GetAll()
                .Select(s=> new SupplierViewModel {
                    Id  = s.Id,
                    Name = s.Name,
                    PhoneNumber = s.PhoneNumber,
                    Adress = s.Address,
                    Email = s.Email
                }); 

            return View(result);
        }

        public Microsoft.AspNetCore.Mvc.ActionResult AddSupplier()
        {
            return View();
        }

        public Microsoft.AspNetCore.Mvc.ActionResult Edit(int id)
        {
            if(id > 0)
            {
                var supplier = _supplier.GetById(id);
                if(supplier != null)
                {
                    var vm = new SupplierViewModel
                    {
                        Id = supplier.Id,
                        Name = supplier.Name,
                        Adress = supplier.Address, 
                        PhoneNumber = supplier.PhoneNumber, 
                        Email = supplier.Email
                    };
                    return View("AddSupplier", vm);
                }
            }
            return View("AddSupplier");
        }
    }
}