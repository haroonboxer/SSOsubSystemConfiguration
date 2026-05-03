    using Application.DTO.Accountdto;
    using Application.IRepository;
    using FluentValidation;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Application.Features.AccountFeatures.Account.Query
    {
        public class RoleDetailQuery:IRequest<RolesDetailDto>
        {
            public string RoleId { get; set; }
        }
        public class RoleDetailValidation:AbstractValidator<RoleDetailQuery>
        {
            public void RoleDetailQuery()
            {
                RuleFor(v=>v.RoleId).NotEmpty().WithMessage("Role id is required");

            }
        }
        public class RoleDetailHandler : IRequestHandler<RoleDetailQuery, RolesDetailDto>
        {
            private readonly IUnitOfWork _unitOfwork;
            public   RoleDetailHandler(IUnitOfWork unitOfWork)
            {
                _unitOfwork = unitOfWork;
            }
            public async Task<RolesDetailDto> Handle(RoleDetailQuery request, CancellationToken cancellationToken)
            {
                var data =await _unitOfwork.Account.LoadRoleClaims(request.RoleId);
                return data;
            }

       
        }
    }
