using System;
using System.Linq;
using ARS.Inventory.Management.Domain.Interfaces;
using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ARS.Inventory.Management.Controllers.Api
{
    [Authorize]
    [Route("api/Suppliers")]
    public class SuppliersController : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        private ISupplierService _supplier;

        public SuppliersController(ISupplierService supplier)
        {
            this._supplier = supplier;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _supplier.GetAll().Select(c => new SupplierViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Adress = c.Adress,
                PhoneNumber = c.PhoneNumber

            });
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var result = _supplier.GetById(id);

            if (result != null)
            {
                SupplierViewModel vm = new SupplierViewModel
                {
                    Id = result.Id,
                    Name = result.Name,
                    Adress = result.Adress,
                    PhoneNumber = result.PhoneNumber
                };
                return Ok(vm);
            }
            return Ok("Item Not Found!");
        }

        [HttpPost]
        public async Task<IActionResult> Insert(SupplierViewModel model)
        {
            try
            {
                Supplier supplier = new Supplier
                {
                    Id = model.Id,
                    Name = model.Name,
                    Adress = model.Adress,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email
                };

                await _supplier.InsertAsync(supplier);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            

        }
        [HttpPut]
        public IActionResult Update(SupplierViewModel model)
        {
            Supplier supplier = new Supplier
            {
                Id = model.Id,
                Name = model.Name,
                Adress = model.Adress,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email
            };

            _supplier.Update(supplier);
            return Ok(model);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _supplier.Delete(id);
            return Ok("Deleted Successfully!");
        }
    }
}
