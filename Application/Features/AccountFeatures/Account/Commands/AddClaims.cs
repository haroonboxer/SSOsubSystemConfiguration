using Application.IRepository;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AccountFeatures.Account.Commands
{
    public class ClaimsCommand:IRequest<string>
    {
        public string RoleId { get; set;  }
        public List<string> selectedPermissions { get; set; }
    }
    public class ClaimsValidation: AbstractValidator<ClaimsCommand>
    {
        public  ClaimsValidation()
        {
            RuleFor(v => v.RoleId).NotEmpty().WithMessage("Role id is required");
            RuleFor(v => v.selectedPermissions).NotEmpty().WithMessage("Rules are required at least one");
        }
    }
    public class CliamsHandler : IRequestHandler<ClaimsCommand, string>
    {
        private readonly IUnitOfWork _wrk;

        public CliamsHandler(IUnitOfWork wrk)        
        {

            _wrk = wrk;
        }
        public Task<string> Handle(ClaimsCommand request, CancellationToken cancellationToken)
        {

            var response = _wrk.Account.AddClaimsToRole(request.RoleId,request.selectedPermissions);
            return response;
        }
    }
}
