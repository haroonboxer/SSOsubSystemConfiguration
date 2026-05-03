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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IHttpContextAccessor _httpContext;

        public AppUserRepository(
            RoleManager<IdentityRole> roleManager,
            AppDbContext db,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IHttpContextAccessor httpContext

        )
        {
            _roleManager = roleManager;
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContext = httpContext;
        }
        public async Task<string> Login(Logindto dto)
        {
            
            var user = await _userManager.FindByNameAsync(dto.Name);

            string ip = _httpContext.HttpContext.Connection.RemoteIpAddress?.ToString();
             if(user != null)
            {
                if(user.Active == true)
                {
                   // var CheckPass =await _userManager.CheckPasswordAsync(user,dto.Password);
                    //if(CheckPass)
                    //{
                        var result =await _signInManager.PasswordSignInAsync(user, dto.Password, true, lockoutOnFailure:true);
                        if(result.Succeeded)
                        {
                            return "User Authentitcation is true";
                        }
                        else if(result.IsLockedOut)
                        {
                            return "Account locked Try again latter";
                        }
                   // }
                    else
                    {
                        return "Incorrent User Name or Password";
                    }
                }
                else
                {
                    return "User is Not Active";
                }
            }
            {
                return "Incorrent User Name or Password";
            }
        }
            public async Task<string> RegisterUser(RegisterationCommand dto)
            {
            var existingUser = await _userManager.FindByNameAsync(dto.Name);
                if (existingUser != null)
                {
               
                        return "User Name is taken";
                }
                if (string.IsNullOrEmpty(dto.RoleId))
                    return "User Role is required";

                try
                {
                    var newUser = new AppUser
                    {
                        Email = dto.Email,
                        UserName = dto.Name,
                        Active = true,
                        Rankid = dto.RankId,
                        PhoneNumber = dto.PhoneNumber,
                        Name = dto.Name,
                        Department_Id = dto.DepartmentId,
                        UserNameInLocalLang = dto.LocalName,
                     
                    };
                var result = await _userManager.CreateAsync(newUser, dto.Password);

                //var result = await _userManager.CreateAsync(newUser, dto.Password);
                //if (!result.Succeeded)
                //    return string.Join(", ", result.Errors.Select(e => e.Description));
                var Role =await _roleManager.FindByIdAsync(dto.RoleId);
                if (Role == null) 
                {
                    return "Role Not Found";
                }
                var roleResult = await _userManager.AddToRoleAsync(newUser, Role.Name);
                    if (!roleResult.Succeeded)
                        return string.Join(", ", roleResult.Errors.Select(e => e.Description));

                    return "User Added Successfully";
                }
                catch (Exception ex)
                {
                    return "خطا در سیستم: " + ex.Message;
                }
            }
        public async Task<GetUserdto> Getuser(string userid)
        {
             
            var Data =await _db.appUsers.Where(e=>e.Id == userid)
                .Select(x=> new GetUserdto
                { 
             Id = x.Id,
             Email = x.Email    ,
            LocalName = x.UserNameInLocalLang,
            DepartmentId = x.Department_Id,
            Name = x.Name,
            PhoneNumber = x.PhoneNumber,
            RankId = x.Rankid
                    }).FirstOrDefaultAsync();

            return Data;
        }
        public async Task<string> EditAppUser(EditCommand command)
        {
            var Appuser =await  _db.appUsers.FindAsync(command.Id);
            if(Appuser==null)
            {
                return "user not found";
            }
            Appuser.PhoneNumber = command.PhoneNumber;
            Appuser.Name = command.Name;
            Appuser.UserName = command.Name;
            Appuser.Email = command.Email;
            Appuser.Rankid = command.RankId;
            Appuser.UserNameInLocalLang = command.LocalName;
             _db.appUsers.Update(Appuser);
            var Result = await _db.SaveChangesAsync();
            if(Result > 0)
            {
                return "User Update";
            }
            else
            {
                return "User not Updated";
            }
        }
        public async Task<string> Logout()
        {
           await _signInManager.SignOutAsync();
            return "SingOut Complete";
        }
        //var data = await _db.ranks.Select(x => new LoadRankdto
        //{
        //    id = x.Id,
        //    Name = x.Name
        //}).ToListAsync();
        public async Task<List<LoadRankdto>> LoadRank()
        {
            var rankId = 1;
            var datas = await _db.Database.SqlQuery<LoadRankdto>($@"select r.Id,r.Name from ranks r").ToListAsync();
            //var para2 = new SqlParameter("@RankId",2);
            return datas;
        }
        public async Task<List<Roledtos>> LoadRoles()
        {
            var Role =await _roleManager.Roles.Select( x=> new Roledtos
            {
                Id = x.Id,
                RoleName = x.Name
            }).ToListAsync();
            return Role;

        }
      
        public async Task<RolesDetailDto> LoadRoleClaims(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            // Retrieve all claims from the database first and then perform grouping in-memory
            var allClaims = _db.AllClaims.ToList(); // Fetch all claims from the database

            var groupedClaims = allClaims.GroupBy(p => p.groupClaims).ToList(); // Group in-memory

            var roleClaims = await _roleManager.GetClaimsAsync(role); // Get claims assigned to this role

            // Check if the role has each claim and prepare the view model
            var modelClaims = groupedClaims.SelectMany(group => group.Select(claim => new Claimsdto
            {
                ClaimType = claim.ClaimType, // Access ClaimsType from each GroupedRoleClaims object
                ClaimValue = roleClaims.Any(rc => rc.Type == claim.ClaimType), // Check if this claim is assigned to the role
                groupClaims = group.Key
            })).ToList();

            var model = new RolesDetailDto
            {
                RoleName = role.Name,
                RoleId = role.Id,
                Claims = modelClaims,  // List of claims and whether they are assigned to the role
                RoleClaims = roleClaims.ToList()
            };

            return model;
        }
        public async Task<string> AddClaimsToRole(string Roleid, List<string> selectedPermission)
        {
            var role = await _roleManager.FindByIdAsync(Roleid);
            if (role == null)
            {
                // Handle the case where the role is not found
                return "Role Not Found";
            }

            // Get existing claims for the role
            var roleClaims = await _roleManager.GetClaimsAsync(role);

            // Remove claims that are present in the role but not selected
            foreach (var roleClaim in roleClaims)
            {
                var hasPermission = selectedPermission.Contains(roleClaim.Type);
                if (!hasPermission)
                {
                    await _roleManager.RemoveClaimAsync(role, roleClaim);
                }
            }

            // Add claims that are selected but not present in the role
            foreach (var selectedPermissions in selectedPermission)
            {
                var hasClaim = roleClaims.Any(roleClaim => roleClaim.Type == selectedPermissions);

                if (!hasClaim)
                {
          
                    var claim = _db.AllClaims.FirstOrDefault(x => x.ClaimType == selectedPermissions);
                    if (claim != null)
                    {
                        await _roleManager.AddClaimAsync(role, new Claim(claim.ClaimType, claim.ClaimValue.ToString()));
                    }
                }
            }
            return "Claims are added";
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
        
        public async Task<string> AddRole(Roledtos dto)
        {
            var Role = new IdentityRole(dto.RoleName);
            var result = await _roleManager.CreateAsync(Role);

            if (!result.Succeeded)
            {
                return "Not Added "; 
            }
            else
            {
                return "Added";
            }
        }
        public async Task<string> EditRole(ChangeUserRoleCommand request)
        {
            var user =await  _userManager.FindByIdAsync(request.UserId);
            var UserCurrentRole =await _userManager.GetRolesAsync(user);
            if (UserCurrentRole.Any())
            {
                
                var RemoveCurrentRole =await  _userManager.RemoveFromRolesAsync(user, UserCurrentRole);
            }
            var UserNewRole =await  _roleManager.FindByIdAsync(request.RoleId);
            var NewRoleResult = await _userManager.AddToRoleAsync(user, UserNewRole.Name);
            return "Role added successfuly";
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
        public async Task<string> ActivateDeActivatedUser( string Userid,bool Activation)
         {
            var data =await _db.appUsers.Where(i => i.Id == Userid).FirstOrDefaultAsync();
            if(data !=null)
            {
                if(data.Active == true)
                {
                    data.Active = false;
                   await _userManager.UpdateAsync(data);
                   await _db.SaveChangesAsync();
                    return "User is De Activated";
                }
                else
                {
                    data.Active = true;
                  await  _userManager.UpdateAsync(data);
                   await _db.SaveChangesAsync();
                    return "User is Activated";
                }
            }
            return "User Not Found";
        }
        public async Task<string> ChangePassword(ChangePasswordCommand request)
        {
            var user =await _userManager.FindByIdAsync(request.UserId);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, request.password);

               
                if (result.Succeeded)
                {
                    // THIS is where your issue actually is
                    return "password changed successfully";
                }
                return string.Join(" | ", result.Errors.Select(e => e.Description));
                
            }
            else
            {
                return "user not found";
            }



        }


      
    }
}

