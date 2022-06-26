using Bwod.Web.Services;
using Bwod.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
var services = builder.Services;
services.AddHttpClient<IProductService, ProductService>
    (
       t => t.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductAPI"])
    );
services.AddHttpClient<ICartService, CartService>
    (
       t => t.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CartAPI"])
    );

services.AddHttpClient<ICouponService, CouponService>
    (
       t => t.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CouponAPI"])
    );

services.AddServerSideBlazor();
services.AddRazorPages();
services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
}).AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
.AddOpenIdConnect("oidc", options =>
{
    options.Authority = builder.Configuration["ServiceUrls:IdentityServer"];
    options.GetClaimsFromUserInfoEndpoint = true;
    options.ClientId = "bwod";
    options.ClientSecret = "genius";
    options.ResponseType = "code";
    options.ClaimActions.MapJsonKey("role", "role", "role");
    options.ClaimActions.MapJsonKey("sub", "sub", "sub");
    options.TokenValidationParameters.NameClaimType = "name";
    options.TokenValidationParameters.RoleClaimType = "role";
    options.Scope.Add("bwod");
    options.SaveTokens = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.MapDefaultControllerRoute();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
