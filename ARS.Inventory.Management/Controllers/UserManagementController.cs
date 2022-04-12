using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Infrastructure.Repository.Context;
using ARS.Inventory.Management.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ARS.Inventory.Management.Controllers
{
    [Authorize(Roles = "Admin,WarehouseManager")]
    public class UserManagementController : Controller
    {
        InventoryDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DbContextOptions _dbContextOptions;

        public UserManagementController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            DbContextOptions<InventoryDbContext> dbContextOptions)
        {
            _dbContext = new InventoryDbContext(dbContextOptions);
            _userManager = userManager;
            _roleManager = roleManager;
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
            if (!string.IsNullOrEmpty(id))
            {
                var user = await _userManager.FindByIdAsync(id);
                var userRoles = await _userManager.GetRolesAsync(user);
                ViewBag.Roles = new SelectList(userRoles, "Name", "Name");
                return View("EditUser");
            }

            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(currentUserId);
            var currentUserRoles = await _userManager.GetRolesAsync(currentUser);
            var roles = _roleManager.Roles.ToList();
            int userRoleId = int.Parse(roles.FirstOrDefault(x => x.Name == currentUserRoles[0])?.Id ?? "999");
            ViewBag.Roles = new SelectList(roles.Where(x => int.Parse(x.Id) >= userRoleId), "Name", "Name");
            
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
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var vm = new UserManagementViewModel
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.Email,
                        RoleName = userRoles[0],
                        RegisteredDate = user.RegisteredDate,
                    };

                    var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    var currentUser = await _userManager.FindByIdAsync(currentUserId);
                    var currentUserRoles = await _userManager.GetRolesAsync(currentUser);
                    var roles = _roleManager.Roles.ToList();
                    int userRoleId = int.Parse(roles.FirstOrDefault(x => x.Name == currentUserRoles[0])?.Id ?? "999");
                    ViewBag.Roles = new SelectList(roles.Where(x => int.Parse(x.Id) >= userRoleId), "Name", "Name");

                    return View("EditUser",vm);
                }
            }
            return View("EditUser");

        }

       
  
    }
}