using ARS.Inventory.Management.Infrastructure.Repository.Context;
using ARS.Inventory.Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ARS.Inventory.Management.Controllers
{
    public class CustomerController : Controller
    {
        InventoryDbContext db;

        public CustomerController(DbContextOptions<InventoryDbContext> dbContextOptions)
        {
            db = new InventoryDbContext(dbContextOptions);
        }

        // GET: Customer
        public IActionResult ListCustomer()
        {
            var result = (from user in db.Users
                          join userRole in db.UserRoles on user.Id equals userRole.UserId
                          join role in db.Roles on userRole.RoleId equals
                          role.Id
                          select new UserManagementViewModel()
                          {
                              Id = user.Id,
                              UserName = user.UserName,
                              //FirstName = user.FirstName,
                              //LastName = user.LastName,
                              PhoneNumber = user.PhoneNumber,
                              Email = user.Email,
                              //CardNumber = user.CardNumber,
                              RegisteredDate = user.RegisteredDate,
                              RoleName = role.Name
                          }).ToList();

            return View(result);
        }

        public IActionResult EditRole(string id)
        {



            return View();
        }

        public IActionResult Deneme()
        {

            var usersWithRoles = (from user in db.Users
                                  join userRole in db.UserRoles on user.Id equals userRole.UserId
                                  join role in db.Roles on userRole.RoleId equals
                                  role.Id
                                  select new UserManagementViewModel()
                                  {
                                      Id = user.Id,
                                      //FirstName = user.FirstName,
                                      //LastName = user.LastName,
                                      UserName = user.UserName,
                                      Email = user.Email,
                                      RoleName = role.Name
                                  }).ToList();



            return View(usersWithRoles);
        }
    }
}