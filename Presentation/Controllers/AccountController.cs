using Application.DTO;
using Application.DTO.Accountdto;
using Application.Features.AccountFeatures;
using Application.Features.AccountFeatures.Account.Commands;
using Application.Features.AccountFeatures.Account.Query;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography.Xml;
using System.Text;
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
        [HttpGet]
        public IActionResult Logins()
        {

            if (TempData["InActiveUser"] != null)
                ViewBag.InActiveData = TempData["InActiveUser"];

            if (TempData["IncorrectUserName"] != null)
                ViewBag.IncorrectUserName = TempData["IncorrectUserName"];

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Login(string Name, string Password)
        {
            var Command = new LoginCommand
            {
                Name = Name,
                Password = Password
            };
            var Result = await _madiat.Send(Command);
            if (Result == "User Authentitcation is true")
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Result == "User is Not Active")
            {
                TempData["InActiveUser"] = Result;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                TempData["IncorrectUserName"] = Result;
                return RedirectToAction("Login", "Account");
            }

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
        [HttpGet]
        public async Task<IActionResult> LoginThrowAPI(string token)
        {
            if (string.IsNullOrEmpty(token))
                return Unauthorized("Token is missing");

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
                    ClockSkew = TimeSpan.Zero // no delay in expiration
                }, out SecurityToken validatedToken);

                // 🔹 Create cookie login
                await HttpContext.SignInAsync(
                    "Cookies",
                    principal
                );

                // 🔹 Redirect inside subsystem
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return Unauthorized("Invalid token");
            }
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> RegisternewUser()
        {
            var query = new RankQuery  
            {
            };
           
            ViewBag.Ranks =await  _madiat.Send(query);
            var RoleQuery = new RolesQuries
            {

            };
            ViewBag.Role = await _madiat.Send(RoleQuery);
            var DepartmentQuery = new DepartmentQuery
            {
            };
            ViewBag.Department = await _madiat.Send(DepartmentQuery);

           
            return View();

        }
        [HttpPost]
        [Authorize]
        public async Task<JsonResult> RegisternewUser(RegisterUsertdo dto)
        {
            var RegisterNewUser = new RegisterationCommand
            { 
                LocalName = dto.LocalName,
                Name = dto.Name,
                DepartmentId = dto.DepartmentId,
                Password = dto.Password,
                PhoneNumber = dto.PhoneNumber,
                RankId = dto.RankId ,
                RoleId = dto.RoleId,
                Email = dto.Email,
            };
            var data =await _madiat.Send(RegisterNewUser);

            return Json(new {success=true });
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> UserList()
        {
            var query = new RankQuery
            {
            };

            ViewBag.Ranks = await _madiat.Send(query);
            var RoleQuery = new RolesQuries
            {

            };
            ViewBag.Role = await _madiat.Send(RoleQuery);
            var DepartmentQuery = new DepartmentQuery
            {
            };
            ViewBag.Department = await _madiat.Send(DepartmentQuery);
            return View();
        }
        [Authorize]
        public async Task<IActionResult> Roles()
        {
            var GetRole = new RolesQuries
            {

            };
            var Result =await _madiat.Send(GetRole);
            ViewBag.Role = Result;
            return View();
        }
        [Authorize]
        public async  Task<JsonResult> AddRole(Roledtos dto)
        {
            var NewRole = new AddRoleCommand
            { 
                
                RoleName = dto.RoleName
            };
            var data =await _madiat.Send(NewRole);

            return Json(new { success=true});
        }
        [Authorize]
        public async Task<JsonResult> LoadRoles()
        {
            var GetRole = new RolesQuries
            {

            };
            var Result = await _madiat.Send(GetRole);
            return Json(Result);
        }
        [Authorize]
        [HttpPost]
        public async Task<JsonResult> ChangeUserRole(ChangeUserRoledto dto)
        {
            var Command = new ChangeUserRoleCommand
            {
                RoleId = dto.RoleId,
                UserId = dto.UserId
                 
            };
            var Result = await _madiat.Send(Command);
            return Json(Result);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> RoleDetail(string RoleId)
        {
            var query = new RoleDetailQuery
            {
                RoleId = RoleId
            };
            var Data = await _madiat.Send(query);
          
            return View(Data);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddOrRemovePermissionToRole(string RoleId,List<string> selectedPermissions)
        {
            var command = new ClaimsCommand
            {
                RoleId=RoleId,
                selectedPermissions = selectedPermissions
            };
            var response = await _madiat.Send(command); 
            return RedirectToAction("RoleDetail", new { RoleId = RoleId });
        }
        //User Functionalities started
        [HttpPost]
        [Authorize]
        public async Task<JsonResult> LoadUser()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length):0;
           // int skip = start != null ? Convert.ToInt32(start) : 0;

            var command = new LoadUserCommand
            { 
          
            startp  =start,
            lengthp = length,
            
            };
            var data = await _madiat.Send(command);

          
            var obj = new 
            {
                draw = draw,
                recordsFiltered = data.recordsFiltered,
                recordsTotal =  data.recordsTotal,
                data = data.data,
            };
            return Json(obj);
        }
        [HttpGet]
        [Authorize]
        public async Task<JsonResult> Getuser(string UserId)
        {
            var query  =new  GetUserQuery{
                UserId = UserId
            };
            var data =await _madiat.Send(query);
            return Json(data);
        }
        [HttpPost]
        [Authorize]
        public async Task<JsonResult> UpdateUser(RegisterUsertdo dto)
        {
            var Command = new EditCommand
            {
                DepartmentId = dto.DepartmentId,
                Email = dto.Email,
                Id = dto.Id,
                LocalName = dto.LocalName,
                Name = dto.Name,
                PhoneNumber = dto.PhoneNumber,
                RankId = dto.RankId,
            };
      

        
            var FinalResult = await _madiat.Send(Command);

            return Json(new{ message = FinalResult });
        }
        [HttpPost]
        [Authorize]
        public async Task<JsonResult> ActivateDeActivatedUser(string UserId,bool Activation)
        {
           
            var command = new UserActivationCommand
            { 
            UserId = UserId,
            IsActive = Activation
            };
            var Result = await _madiat.Send(command);
            
            return Json(Result);
        }
  
        [HttpPost]
        [Authorize]
        public async Task<JsonResult> ChangePassword(ChangePassworddto request)
        {
            var command = new ChangePasswordCommand
            {
                UserId = request.UserId,
                password = request.password,
                confirmPassword = request.confirmPassword,
            };
            var result = await _madiat.Send(command);

            return Json(result);
        }
      
     
    }
}
