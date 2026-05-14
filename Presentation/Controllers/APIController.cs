using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Buffers.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;

namespace Presentation.Controllers
{
   
    
    public class APIController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpcontext;
        private readonly IConfiguration _configuration;
        public APIController(HttpClient httClient, IHttpContextAccessor httpcontext,IConfiguration configuration) 
        {
            _httpClient = httClient;
            _httpcontext = httpcontext;
            _configuration = configuration;
        }
        public async Task<IActionResult> LoadProjects()
        {
            var BaseURL = _configuration["ApiSettings:UserServiceBaseUrl"];

            var UserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var token = _httpcontext.HttpContext.Request.Cookies["access_token"];

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsJsonAsync(
                $"{BaseURL}/Project/LoadProjectAPI",
                UserId
            );

            var data = await response.Content.ReadAsStringAsync();

            return Content(data, "application/json");
        }
       
    }
}
