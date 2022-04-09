using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Domain.Interfaces;
using ARS.Inventory.Management.Web.Models;

namespace ARS.Inventory.Management.Controllers.Api
{
    [Authorize]
    [Route("api/Categories")]
    public class CategoriesController : Controller
    {
        ICategoryService _category;
        public CategoriesController(ICategoryService category)
        {
            this._category = category;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _category.GetAll().Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name
            });
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var result = _category.GetById(id);

            if(result != null)
            {
                CategoryViewModel vm = new CategoryViewModel
                {
                    Id = result.Id,
                    Name = result.Name
                };
                return Ok(vm);
            }
            return Ok(HttpStatusCode.NotFound);
        }

        [HttpPost]
        public IActionResult Insert(CategoryViewModel model)
        {
            Category category = new Category
            {
                Name = model.Name
            };

            _category.Insert(category);
            return Ok(category);
        }

        [HttpPut]
        public IActionResult Update(CategoryViewModel model)
        {
            Category category = new Category
            {
                Id = model.Id,
                Name = model.Name
            };

            _category.Update(category);
            return Ok(model);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _category.Delete(id);
            return Ok("Deleted Successfully !");
        }

    }
}
