using ARS.Inventory.Management.Domain.Interfaces;
using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Web.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ARS.Inventory.Management.Controllers.Api
{
    [Authorize]
    [Route("/api/Products")]
    public class ProductsController : Controller
    {
        private IProductService _product;
        private readonly IMapper _mapper;

        public ProductsController(IProductService product, IMapper mapper)
        {
            this._product = product;
            _mapper = mapper;
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

        [Route("/api/Products/ProductsByCategory")]
        [HttpGet]
        public async Task<JsonResult> ProductsByCategory(string categoryId)
        {
            if (int.TryParse(categoryId, out int catId))
            {
                var products = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductsViewModel>>(await _product.GetAsync(x => x.CategoryId == catId));
                return Json(products);
            }

            return Json(null);

        }

        [Route("/api/Products/ProductsByCriteria")]
        [HttpGet]
        public async Task<JsonResult> ProductsByCriteria(string query, string page, string categoryId)
        {
            if (string.IsNullOrWhiteSpace(query))
                return Json(null);

            int pageNum = 0;
            int.TryParse(page, out pageNum);
            int skip = pageNum * 10;
            Expression<Func<Product, bool>> filter = int.TryParse(categoryId, out int catId) 
                ? x => x.CategoryId == catId && x.Name.ToLower().Contains(query.ToLower())
                : x => x.Name.ToLower().Contains(query.ToLower());

            var items =  _mapper.Map<IEnumerable<Product>, IEnumerable<ProductsViewModel>>(await _product.GetPagedAsync(filter, skip));
            var total_count = await _product.GetCountAsync(filter);



            return Json(new { items, total_count });
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
