using Application.DTO.Accountdto;
using Application.DTO.Departmentdto;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepository
{
    public interface IDepartmentsRepository 
    {
        public Task<DataTableReturnType> LoadDepartment(int start,int length,string BaseURL);
    }
}
