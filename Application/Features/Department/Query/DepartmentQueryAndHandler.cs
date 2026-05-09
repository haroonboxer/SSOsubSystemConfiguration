using Application.DTO.Accountdto;
using Application.DTO.Departmentdto;
using Application.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Department.Query
{
    public class DepartmentListQuery:IRequest<DataTableReturnType>
    {
        public int start { get; set; }
        public int length { get; set; }
        public string BaseURL { get; set; }

    }
    public class DepartmentHandler : IRequestHandler<DepartmentListQuery, DataTableReturnType>
    {
        private readonly IUnitOfWork _db;
        public DepartmentHandler(IUnitOfWork db) 
        { 
            _db = db;
        }
      public async Task<DataTableReturnType> Handle(DepartmentListQuery request,CancellationToken cancellationtoken)
      {
            var data =await _db.Departments.LoadDepartment(request.start,request.length,request.BaseURL);
            return data; 
      }

        

    }

}
