using ARS.Inventory.Management.Infrastructure.Repository.Context;
using ARS.Inventory.Management.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ARS.Inventory.Management.Controllers.Api
{
    [Authorize]
    public class RolesController : Controller
    {
        RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }



        [HttpGet]
        public async Task<IActionResult> GetById(string Id)
        {
            var result = await _roleManager.FindByIdAsync(Id);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel role)
        {
            bool result = await _roleManager.RoleExistsAsync(role.RoleName);

            if (result != true)
            {
                IdentityRole x = new IdentityRole
                {
                    Name = role.RoleName
                };
                await _roleManager.CreateAsync(x);
                return Ok("Role was created successfuly");
            }

            return BadRequest("This role is exist");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole(RoleViewModel role)
        {
            IdentityRole model = new IdentityRole
            {
                Id = role.Id,
                Name = role.RoleName
            };
            await _roleManager.UpdateAsync(model);
            return Ok("Role was updated successfuly");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var result = await _roleManager.FindByIdAsync(id);
            await _roleManager.DeleteAsync(result);
            return Ok("Role was deleted successfuly");
        }

    }
}