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

        public AppUserRepository(IHttpContextAccessor httpContext,AppDbContext db)
        {
          
            _httpContext = httpContext;
            _db = db;
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
                AddedBy = dto.AddedBy

            };
            _db.Departments.AddAsync(data);
            var result = await _db.SaveChangesAsync();
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
 


      
    }
}

