
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

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.Cookie.Name = ".AspNetCore.Cookies";

    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.None;

    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromHours(2);

    options.LoginPath = null;

    options.Events.OnRedirectToLogin = context =>
    {
        var ssoLoginUrl = "https://localhost:7161/";
        var returnUrl = Uri.EscapeDataString(context.Request.GetDisplayUrl());

        context.Response.Redirect($"{ssoLoginUrl}?redirectUrl={returnUrl}");
        return Task.CompletedTask;
    };

    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        return Task.CompletedTask;
    };
});

builder.Services.AddAuthorization();
// Add localization services
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
 
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
 
    // Configure the HTTP request pipeline
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }
 

 
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Logins}/{id?}");
//app.MapGet("/",()=>"Hello world");
app.Run();
