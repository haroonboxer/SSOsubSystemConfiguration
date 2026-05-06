using Application.DTO.Accountdto;
using Application.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Department.Command
{
    public class DepartmenCommand:IRequest<string>
    {
        public string? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string? AddedBy { get; set; }
    }
    public class DepartmentHandler : IRequestHandler<DepartmenCommand, string>
    {
        IUnitOfWork _unitOfWork;
        public DepartmentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(DepartmenCommand request, CancellationToken cancellationToken)
        {
            var data = new Departmentdto
            {
                Id = request.DepartmentId,
                DepartmentName = request.DepartmentName,
                AddedBy = request.AddedBy,  
            };
           var result =await  _unitOfWork.Account.AddDepartment(data);
            return result;
        }
    }
}
