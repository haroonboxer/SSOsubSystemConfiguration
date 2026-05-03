using Application.DTO.Accountdto;
using Application.Features.AccountFeatures.Account.Commands;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepository
{
    public interface IAppUser
    {
        public Task<string> Login(Logindto dto);
        public Task<string> RegisterUser(RegisterationCommand dto);
        public Task<string> Logout();
        public Task<List<LoadRankdto>> LoadRank();
        public  Task<List<Roledtos>> LoadRoles();
        public Task<List<Departmentdto>> LoadDepartment();
        public Task<string> AddRole(Roledtos dto);
        public Task<string> EditRole(ChangeUserRoleCommand modal);
        public Task<string> AddDepartment(Departmentdto dto);
        public Task<LoadUserdto> LoadUser(string startp,string lengthp);
        public Task<string> EditAppUser(EditCommand command);
        public Task<GetUserdto> Getuser(string userid);
        public  Task<RolesDetailDto> LoadRoleClaims(string roleId);
        public Task<string> AddClaimsToRole(string Roleid, List<string> selectedPermission);
        public Task<string> ActivateDeActivatedUser(string Userid, bool Activation);
        public Task<string> ChangePassword(ChangePasswordCommand request);



    }
}
