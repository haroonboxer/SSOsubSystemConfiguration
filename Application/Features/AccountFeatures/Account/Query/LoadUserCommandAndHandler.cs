using Application.DTO.Accountdto;
using Application.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AccountFeatures.Account.Query
{
 
    public class LoadUserCommand:IRequest<LoadUserdto>
    {
   
        public string startp { get; set; }
        public string lengthp { get; set; }
    }
    public class LoadUserQuery : IRequestHandler<LoadUserCommand, LoadUserdto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public LoadUserQuery(IUnitOfWork    unitofwork)
        {
            _unitOfWork = unitofwork;
        }
        public async  Task<LoadUserdto> Handle(LoadUserCommand request, CancellationToken cancellationToken)
        {
            var data =await _unitOfWork.Account.LoadUser(request.startp,request.lengthp);
            return data;
        }
    }

}
