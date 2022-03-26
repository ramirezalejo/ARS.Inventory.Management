using ARS.Inventory.Management.Domain.Interfaces;
using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ARS.Inventory.Management.Controllers.Api
{
    [Authorize]
    [Route("/api/Products")]
    public class ProductsController : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        private IProductService _product;
        public ProductsController(IProductService product)
        {
            this._product = product;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _product.GetAll().Select(x => new ProductsViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                PurchasingPrice = x.PurchasingPrice,
                SellingPrice = x.SellingPrice,
                Quantity = x.Quantity,
                CategoryId = x.CategoryId,
                SupplierId = x.SupplierId,
                CategoryName = x.Category.Name
            });

            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var result = _product.GetById(id);
            if (result != null)
            {
                var vm = new ProductsViewModel
                {
                    Id = result.Id,
                    Name = result.Name,
                    Code = result.Code,
                    PurchasingPrice = result.PurchasingPrice,
                    SellingPrice = result.SellingPrice,
                    Quantity = result.Quantity,
                    CategoryId = result.CategoryId,
                    SupplierId = result.SupplierId
                };
                return Ok(vm);
            }
            return Ok("Item Not Found !");
        }

        [HttpPost]
        public IActionResult Insert(ProductsViewModel model)
        {
            Product product = new Product
            {
                Id = model.Id,
                Name = model.Name,
                Code = model.Code,
                PurchasingPrice = model.PurchasingPrice,
                SellingPrice = model.SellingPrice,
                Quantity = model.Quantity,
                CategoryId = model.CategoryId,
                SupplierId = model.SupplierId
            };
            _product.Insert(product);
            return Ok(product);
        }

        [HttpPut]
        public IActionResult Update(ProductsViewModel model)
        {
            Product product = new Product
            {
                Id = model.Id,
                Name = model.Name,
                Code = model.Code,
                PurchasingPrice = model.PurchasingPrice,
                SellingPrice = model.SellingPrice,
                Quantity = model.Quantity,
                CategoryId = model.CategoryId,
                SupplierId = model.SupplierId
            };
            _product.Update(product);
            return Ok(product);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _product.Delete(id);
            return Ok("Deleted Successfully !");
        }
    }
}
