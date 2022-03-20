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


        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Purchase> Purchase { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

    }
}
