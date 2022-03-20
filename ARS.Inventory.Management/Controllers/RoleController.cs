using ARS.Inventory.Management.Infrastructure.Repository.Context;
using ARS.Inventory.Management.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ARS.Inventory.Management.Controllers
{
    public class RoleController : Controller
    {
        RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult RoleList()
        {
            var result = _roleManager.Roles.ToList();
            List<RoleViewModel> model = new List<RoleViewModel>();

            foreach (var item in result)
            {
                model.Add(new RoleViewModel { Id = item.Id, RoleName = item.Name });
            }

            return View(model);
        }

        public IActionResult EditRole(string id)
        {
            var result = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            if (result == null) return View("Error");

            RoleViewModel model = new RoleViewModel
            {
                Id = result.Id,
                RoleName = result.Name
            };

            return View(model);
        }
    }
}