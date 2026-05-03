using Application.DTO;
using Application.DTO.Accountdto;
using Application.Features.AccountFeatures;
using Application.Features.AccountFeatures.Account.Commands;
using Application.Features.AccountFeatures.Account.Query;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
namespace Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMediator _madiat;
        private readonly IConfiguration _configuration;
        public AccountController(IMediator mediat, IConfiguration configuration)
        {
            _madiat = mediat;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            
            return View();
        }
    
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            var command = new SignOutCommand
            { };
            var Result = await _madiat.Send(command);
            if(Result == "SingOut Complete")
            {
                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction();
            }

        }

        public async Task<IActionResult> LoginThrowAPI(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                var ssoLoginUrl = "https://localhost:7161/"; // your SSO

                var returnUrl = Uri.EscapeDataString(
                    $"{Request.Scheme}://{Request.Host}{Request.Path}"
                );

                return Redirect($"{ssoLoginUrl}?redirectUrl={returnUrl}");
            }

            var key = Encoding.UTF8.GetBytes("Policy-Management-Secrete-Key-2026");
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "ProjectA",

                    ValidateAudience = true,
                    ValidAudience = "AllProjects",

                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                }, out _);

                var identity = principal.Identity as ClaimsIdentity;

                if (identity == null)
                    return Unauthorized("Invalid identity");

                // ✅ NEW COOKIE IDENTITY
                var claimsIdentity = new ClaimsIdentity(
                    CookieAuthenticationDefaults.AuthenticationScheme
                );

                // -----------------------------
                // 1. Copy basic claims safely
                // -----------------------------
                foreach (var claim in identity.Claims)
                {
                    if (claim.Type == ClaimTypes.Name)
                        claimsIdentity.AddClaim(claim);
                }

                // -----------------------------
                // 2. Roles
                // -----------------------------
                var roleJson = identity.FindFirst("Role")?.Value;

                if (!string.IsNullOrEmpty(roleJson))
                {
                    var roles = JsonSerializer.Deserialize<List<string>>(roleJson);

                    foreach (var role in roles)
                    {
                        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
                    }
                }

                // -----------------------------
                // 3. Permissions (FIXED PART)
                // -----------------------------
                var roleClaimsJson = identity.FindFirst("role_claims")?.Value;

                if (!string.IsNullOrEmpty(roleClaimsJson))
                {
                    var roleClaims = JsonSerializer.Deserialize<List<RoleClaimDto>>(roleClaimsJson);

                    foreach (var claim in roleClaims)
                    {
                        if (claim.ClaimValue?.ToString().ToLower() == "true")
                        {
                            claimsIdentity.AddClaim(
                                new Claim("Permission", claim.ClaimType.Trim())
                            );
                        }
                    }
                }

                // -----------------------------
                // 4. Sign in
                // -----------------------------
                var principalToSign = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principalToSign,
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2)
                    });

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return Unauthorized("Invalid token");
            }
        }

      
    
   
       
       
      
     
    }
}
