using Bwod.IdentityServer.Configuration;
using Bwod.IdentityServer.Model;
using Bwod.IdentityServer.Model.Context;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Bwod.IdentityServer.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly MySQLContext _context;
        private readonly UserManager<ApplicationUser> _user;
        private readonly RoleManager<IdentityRole> _role;

        public DbInitializer(MySQLContext context, UserManager<ApplicationUser> user, RoleManager<IdentityRole> role)
        {
            _context = context;
            _user = user;
            _role = role;
        }

        public void Initialize()
        {
            if (_role!.FindByNameAsync(IdentityConfiguration.Admin).Result != null)
                return;

            _role!.CreateAsync(new IdentityRole(IdentityConfiguration.Admin)).GetAwaiter().GetResult();
            _role!.CreateAsync(new IdentityRole(IdentityConfiguration.Client)).GetAwaiter().GetResult();

            ApplicationUser admin = new ApplicationUser()
            {
                UserName = "admin",
                Email = "lucasghank@hotmail.com",
                EmailConfirmed = true,
                PhoneNumber = "+55 (45) 99829-5463",
                firstname = "Lucas",
                lastname = "Hanke Admin"
            };
            _user!.CreateAsync(admin, "$Scope123").GetAwaiter().GetResult();
            _user.AddToRoleAsync(admin, IdentityConfiguration.Admin).GetAwaiter().GetResult();
            var adminClaims = _user.AddClaimsAsync(admin, new Claim[] 
            {
                new Claim(JwtClaimTypes.Name, $"{admin.firstname} {admin.lastname}"),
                new Claim(JwtClaimTypes.GivenName, admin.firstname),
                new Claim(JwtClaimTypes.FamilyName, admin.lastname),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin)
            }).Result;

            ApplicationUser client = new ApplicationUser()
            {
                UserName = "client",
                Email = "lucasghank@hotmail.com",
                EmailConfirmed = true,
                PhoneNumber = "+55 (45) 99829-5463",
                firstname = "Lucas",
                lastname = "Hanke Client"
            };
            _user!.CreateAsync(client, "$Scope123").GetAwaiter().GetResult();
            _user.AddToRoleAsync(admin, IdentityConfiguration.Client).GetAwaiter().GetResult();
            var clientClaims = _user.AddClaimsAsync(client, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{client.firstname} {client.lastname}"),
                new Claim(JwtClaimTypes.GivenName, client.firstname),
                new Claim(JwtClaimTypes.FamilyName, client.lastname),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client)
            }).Result;
        }
    }
}
