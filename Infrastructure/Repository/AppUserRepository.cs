using Application.DTO.Accountdto;
using Application.Features.AccountFeatures.Account.Commands;
using Application.IRepository;
using Domain;
using Infrastructure.Data;
using Infrastructure.Data.Authentications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.InteropServices;
using System.Security.Claims;
namespace Infrastructure.Repository
{
    public class AppUserRepository : IAppUser
    {
 
        private readonly AppDbContext _db;
 
        private readonly IHttpContextAccessor _httpContext;

        public AppUserRepository(
  
            IHttpContextAccessor httpContext

        )
        {
          
            _httpContext = httpContext;
        }
     
      
   
        public async Task<string> Logout()
        {
           //await _signInManager.SignOutAsync();
            return "SingOut Complete";
        }
      
     
        public async Task<List<Departmentdto>> LoadDepartment()
        {
            var Departments = await _db.Departments.Select(
                x=>new Departmentdto
                {
                    Id = x.DepartmentId,
                    DepartmentName = x.DepartmentName,
                }).ToListAsync();
            return Departments;
        }
        
     
        
        public async Task<string> AddDepartment(Departmentdto dto)
        {
            var data = new Department
            {
                DepartmentId = Guid.NewGuid().ToString(),
                DepartmentName = dto.DepartmentName,
            };
            _db.Departments.Add(data);
            var result = _db.SaveChanges();
            return "Info added successfuly";
        }

        public async Task<LoadUserdto> LoadUser(string startp, string lengthp)
        {
           
            int pageSize = !string.IsNullOrEmpty(lengthp) ? Convert.ToInt32(lengthp) : 10;
            int skip = !string.IsNullOrEmpty(startp) ? Convert.ToInt32(startp) : 0;

            var totalRecords = await _db.Users.CountAsync();
           
            var UserData = await _db.Database.SqlQuery<UserListDto>
                (@$"SELECT u.Id,Email,u.UserName Name,u.Active ActiveState ,d.DepartmentName Department ,r.Name rank ,Roles.Name Role
                                                                    FROM  AspNetUsers u 
                                                                    inner join ranks r on u.Rankid = r.Id
                                                                    inner join Departments d on d.DepartmentId = u.Department_Id
                                                                    inner join AspNetUserRoles ur on u.Id = ur.UserId
                                                                    inner join AspNetRoles Roles on Roles.Id = ur.RoleId")
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
            var userList = new List<UserListDto>();

    

            return new LoadUserdto
            {
              
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = UserData
            };
        }
        //public async Task<string> ActivateDeActivatedUser( string Userid,bool Activation)
        // {
        //    var data =await _db.appUsers.Where(i => i.Id == Userid).FirstOrDefaultAsync();
        //    if(data !=null)
        //    {
        //        if(data.Active == true)
        //        {
        //            data.Active = false;
        //           await _userManager.UpdateAsync(data);
        //           await _db.SaveChangesAsync();
        //            return "User is De Activated";
        //        }
        //        else
        //        {
        //            data.Active = true;
        //          await  _userManager.UpdateAsync(data);
        //           await _db.SaveChangesAsync();
        //            return "User is Activated";
        //        }
        //    }
        //    return "User Not Found";
        //}
        //public async Task<string> ChangePassword(ChangePasswordCommand request)
        //{
        //    var user =await _userManager.FindByIdAsync(request.UserId);
        //    if (user != null)
        //    {
        //        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //        var result = await _userManager.ResetPasswordAsync(user, token, request.password);

               
        //        if (result.Succeeded)
        //        {
        //            // THIS is where your issue actually is
        //            return "password changed successfully";
        //        }
        //        return string.Join(" | ", result.Errors.Select(e => e.Description));
                
        //    }
        //    else
        //    {
        //        return "user not found";
        //    }



        //}


      
    }
}

