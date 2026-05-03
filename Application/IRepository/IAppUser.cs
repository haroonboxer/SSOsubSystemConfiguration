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
         public Task<string> Logout();
         public Task<List<Departmentdto>> LoadDepartment();
         public Task<string> AddDepartment(Departmentdto dto);
 
 



    }
}
