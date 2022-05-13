using ARS.Inventory.Management.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ARS.Inventory.Management.Infrastructure.Repository.Context
{
    public class InventoryDbContext : IdentityDbContext<ApplicationUser>
    {

        public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
                : base(options)
        {
        }
    }
}
