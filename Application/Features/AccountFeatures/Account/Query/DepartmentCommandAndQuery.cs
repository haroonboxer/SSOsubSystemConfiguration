using Application.DTO.Accountdto;
using Application.IRepository;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AccountFeatures.Account.Query
{
    public  class DepartmentQuery : IRequest<List<Departmentdto>>
    {
    }
    public class DepartmentQueryHandler : IRequestHandler<DepartmentQuery, List<Departmentdto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentQueryHandler(IUnitOfWork unitOfWork) 
        { 
            _unitOfWork = unitOfWork;

        }
        public async Task<List<Departmentdto>> Handle(DepartmentQuery request, CancellationToken cancellationToken)
        {
            var data =await _unitOfWork.Account.LoadDepartment();

            return data;
        }
    }
}
