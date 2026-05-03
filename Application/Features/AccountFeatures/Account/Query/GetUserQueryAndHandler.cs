using Application.DTO.Accountdto;
using Application.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AccountFeatures.Account.Query
{
    public class GetUserQuery:IRequest<GetUserdto>
    {
        public string UserId { get; set; }

    }
    
    public class GetuserQueryHandler : IRequestHandler<GetUserQuery, GetUserdto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetuserQueryHandler(IUnitOfWork unitOfwork)
        {
            _unitOfWork = unitOfwork;
        }
        public async Task<GetUserdto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var data =await _unitOfWork.Account.Getuser(request.UserId);
            return data;
        }
    }
}
