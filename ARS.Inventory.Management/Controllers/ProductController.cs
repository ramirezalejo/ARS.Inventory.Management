using ARS.Inventory.Management.Domain.Interfaces;
using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ARS.Inventory.Management.Controllers
{
    public class ProductController : Controller
    {
        private ICategoryService _category;
        private ISupplierService _supplier;
        private IProductService _product;
        private readonly IMapper _mapper;

        public ProductController(ICategoryService category, 
            ISupplierService supplier, 
            IProductService product,
            IMapper mapper)
        {
            _category = category;
            _supplier = supplier;
            _product = product;
            _mapper = mapper;
        }

        // GET: Product
        public IActionResult ListProduct()
        {
            var result = _product.GetAll().Select(x => new ProductViewModel
            {

                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                PurchasingPrice = x.PurchasingPrice,
                SellingPrice = x.SellingPrice,
                Category = x.Category,
                Supplier = x.Supplier,
                Quantity = x.Quantity
            });

            return View(result);
        }

        public IActionResult AddProduct()
        {

            var category = _category.GetAll().ToList();
            ViewBag.Category = new SelectList(category, "Id", "Name");

            var supplier = _supplier.GetAll().ToList();
            ViewBag.Supplier = new SelectList(supplier, "Id", "Name");

            return View();
        }

        public IActionResult EditProduct(int id)
        {
            if (id > 0)
            {
                var product = _product.GetById(id);
                if (product != null)
                {
                    var vm = new ProductViewModel
                    {
                        Id = product.Id,
                        Code = product.Code,
                        Name = product.Name,
                        CategoryId = product.CategoryId,
                        SupplierId = product.SupplierId,
                        PurchasingPrice = product.PurchasingPrice,
                        SellingPrice = product.SellingPrice,
                        Quantity = product.Quantity

                    };

                    var category = _category.GetAll().ToList();
                    ViewBag.Category = new SelectList(category, "Id", "Name");

                    var supplier = _supplier.GetAll().ToList();
                    ViewBag.Supplier = new SelectList(supplier, "Id", "Name");
                    return View("AddProduct", vm);

                }

            }

            return View("AddProduct");
        }


        [HttpPost]
        public IActionResult AddProduct(ProductViewModel model)
        {
            var result = _mapper.Map<ProductViewModel, Product>(model);
            _product.Insert(result);
            return View();
        }

        public IActionResult CategoryList()
        {
            var result = _category.GetAll()
                 .Select(m => new CategoryViewModel
                 {
                     Id = m.Id,
                     Name = m.Name
                 });

            return View(result);
        }

        public IActionResult EditCategory(int id)
        {
            var result = _category.GetById(id);

            CategoryViewModel model = new CategoryViewModel
            {
                Id = result.Id,
                Name = result.Name
            };
            return View("EditCategory", model);
        }

    }
}