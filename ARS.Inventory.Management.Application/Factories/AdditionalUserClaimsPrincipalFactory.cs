using ARS.Inventory.Management.Domain.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace ARS.Inventory.Management.Application.Factories
{
    public class AdditionalUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public AdditionalUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        { }

        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);
            //var identity = (ClaimsIdentity)principal.Identity;

            //var claims = new List<Claim>();
            //if (user.CardNumber.StartsWith("6474253814"))
            //{
            //    claims.Add(new Claim(JwtClaimTypes.Role, "Admin"));
            //}

            //identity.AddClaims(claims);
            return principal;
        }
    }
}
