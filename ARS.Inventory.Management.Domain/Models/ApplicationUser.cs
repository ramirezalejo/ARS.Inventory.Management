using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ARS.Inventory.Management.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string CardNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegisteredDate { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = new ClaimsIdentity(await manager.GetClaimsAsync(this));

            // Add custom user claims here
            return userIdentity;
        }

        public virtual ICollection<Order> Orders { get; set; }

    }
}
