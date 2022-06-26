using Microsoft.AspNetCore.Identity;

namespace Bwod.IdentityServer.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string firstname { get; set; } = "";
        public string lastname { get; set; } = "";
    }
}
