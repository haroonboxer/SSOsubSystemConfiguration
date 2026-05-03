
using Application;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Data.Seeder;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;
var builder = WebApplication.CreateBuilder(args);

// Add localization services
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
//builder.Services.AddAuthorization(options =>
//{
//    using (var scope = builder.Services.BuildServiceProvider().CreateScope())
//    {
//        var context = scope.ServiceProvider.GetRequiredService<APDbContext>();
//        var groupedClaimsService = new ApplicationClaims(context);
//        var claims = groupedClaimsService.applicationClaims();

//        foreach (var claim in claims)
//        {
//            // Assuming ClaimValue is a string that needs to be checked against "True"
//            if (claim.ClaimValue == "True")
//            {
//                options.AddPolicy(claim.ClaimsType, policy => policy.RequireClaim(claim.ClaimsType, "True"));
//            }
//        }
//    }
//});
// Configure localization options
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new List<CultureInfo>
    {
        new CultureInfo("ps-AF"),
        new CultureInfo("fa-IR"),
        new CultureInfo("en-US")
    };


    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
});

 

builder.Services.AddControllersWithViews()
    .AddViewLocalization() // Add view localization support
    .AddDataAnnotationsLocalization(); // Add data annotations localization support

builder.Services.Application();


builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())  // Use app.Services to create a scope
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

  //  dbContext.Database.Migrate();  // Applies any pending migrations to the database
}
//using (var scop = app.Services.CreateAsyncScope())
//{
//    await AppUserseeder.Seed(scop.ServiceProvider);
//}
    // Configure the HTTP request pipeline
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        // Cookie name
        options.Cookie.Name = "PolicyAuthCookie";

        // 🔥 IMPORTANT for SSO (cross-site redirect login)
        options.Cookie.SameSite = SameSiteMode.None;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

        // Security settings
        options.Cookie.HttpOnly = true;

        // Session control
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromHours(2);

        // Fallback login path (not used in SSO flow usually)
        options.LoginPath = null;

        // 🔥 Redirect to your SSO server instead of local login page
        options.Events.OnRedirectToLogin = context =>
        {
            var ssoLoginUrl = "https://localhost:7161/"; // ✅ ROOT ONLY

            var returnUrl = Uri.EscapeDataString(context.Request.GetDisplayUrl());

            context.Response.Redirect($"{ssoLoginUrl}?redirectUrl={returnUrl}");
            return Task.CompletedTask;
        };

        // Optional: access denied handling
        options.Events.OnRedirectToAccessDenied = context =>
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return Task.CompletedTask;
        };
    }); 
app.UseAuthentication();
app.UseAuthorization();
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    try
//    {
//        await UserRoleSeeder.SeedRoles(app);
//    }
//    catch (Exception ex)
//    {
//        // Handle exceptions (e.g., log the error)
//    }
//}
//using (var scop = app.Services.CreateScope())
//{
//    var service = scop.ServiceProvider;
//    try
//    {
//        await DatabaseSeeder.SeedDefaultUserAndRole(app);
//    }
//    catch (Exception ex)
//    {
//        // Handle exceptions (e.g., log the error)
//    }
//}
// Use request localization
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Make sure this is before UseAuthorization
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Logins}/{id?}");
//app.MapGet("/",()=>"Hello world");
app.Run();
