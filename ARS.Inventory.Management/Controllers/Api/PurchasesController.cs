using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using ARS.Inventory.Management.Domain.Interfaces;
using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ARS.Inventory.Management.Controllers.Api
{
    [Authorize]
    [Route("/api/Purchases")]
    public class PurchasesController : Controller
    {
        private IPurchaseService _purchase;

        public PurchasesController(IPurchaseService purchase)
        {
            this._purchase = purchase;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _purchase.GetAll().Select(x => new PurchaseViewModel
            {
                Id = x.Id,
                ProductId = x.ProductId,
                UserId = x.UserId,
                CreatedTime = x.CreatedTime,
                Quantity = x.Quantity
            });
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var result = _purchase.GetById(id);

            if (result != null)
            {
                PurchaseViewModel vm = new PurchaseViewModel
                {
                    Id = result.Id,
                    ProductId = result.ProductId,
                    UserId = result.UserId,
                    CreatedTime = result.CreatedTime,
                    Quantity = result.Quantity
                };

                return Ok(vm);
            }
            return Ok("Item Not Found !");
        }
        [HttpPost]
        public async Task<IActionResult> Insert(PurchaseViewModel model)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (user == null) return BadRequest();

            Purchase purchase = new Purchase
            {
                Id = model.Id,
                ProductId = model.ProductId,
                UserId = user,
                Quantity = model.Quantity,
                CreatedTime = DateTime.Now,
                DeliveryTime = model.DeliveryTime,
                Description = model.Description
            };
            _purchase.Insert(purchase);

            return Ok(purchase);
        }
        [HttpPut]
        public IActionResult Update(PurchaseViewModel model)
        {
            Purchase purchase = new Purchase
            {
                Id = model.Id,
                ProductId = model.ProductId,
                UserId = model.UserId,
                Quantity = model.Quantity,
                DeliveryTime = model.DeliveryTime,
                Description = model.Description
            };
            _purchase.Update(purchase);
            return Ok(purchase);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _purchase.Delete(id);
            return Ok("Deleted Successfully !");
        }

        [HttpPost]
        [Route("ConfirmPurchases")]
        public IActionResult ConfirmPurchases(int id)
        {
            _purchase.ConfirmPurchase(id);
            return Ok("Confirmed Successfully");
        }

        [HttpPost]
        [Route("UnconfirmPurchases")]
        public IActionResult UnconfirmPurchases(int id)
        {
            _purchase.UnconfirmPurchase(id);
            return Ok("Unconfirmed Successfully");
        }
    }
}
