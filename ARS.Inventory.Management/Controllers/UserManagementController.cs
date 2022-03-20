using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Infrastructure.Repository.Context;
using ARS.Inventory.Management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ARS.Inventory.Management.Controllers
{
    public class UserManagementController : Controller
    {
        InventoryDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DbContextOptions _dbContextOptions;

        public UserManagementController(UserManager<ApplicationUser> userManager,
            DbContextOptions<InventoryDbContext> dbContextOptions)
        {
            _dbContext = new InventoryDbContext(dbContextOptions);
            _userManager = userManager;
            _dbContextOptions = dbContextOptions;
        }

        // GET: Account
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [Authorize]
        public IActionResult MyAccount()
        {
            return View();
        }

        [Authorize]
        public IActionResult UserList()
        {
            var result = (from user in _dbContext.Users
                          join userRole in _dbContext.UserRoles on user.Id equals userRole.UserId
                          join role in _dbContext.Roles on userRole.RoleId  equals role.Id
                          select new UserManagementViewModel()
                          {
                              Id = user.Id,
                              UserName = user.UserName,
                              PhoneNumber = user.PhoneNumber,
                              Email = user.Email,
                              RegisteredDate = user.RegisteredDate,
                              RoleName = role.Name
                          }).ToList();

            return View(result);
        }


        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            ViewBag.Roles = new SelectList(roles, "Name", "Name");

            //List<SelectListItem> items = new List<SelectListItem>();
            //foreach(var item in roles)
            //{
            //    items.Add(new SelectListItem { Text = item.Name, Value = item.Name });
            //}


            return View("EditUser");
        }

        [Authorize]
        public async Task<IActionResult> UpdateUser(string id)
        {
            if (id != null)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    var roleName = _userManager.GetRolesAsync(user);
                    var vm = new UserManagementViewModel
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.Email,
                        RoleName = (await roleName)[0]
                    };

                    var roles = await _userManager.GetRolesAsync(user);
                    //ViewData["Roles"] = new SelectList(roles, null, "Name");
                    ViewBag.Roles = new SelectList(roles, "Name", "Name");

                    return View("EditUser",vm);
                }
            }
            return View("EditUser");

        }

       
  
    }
}