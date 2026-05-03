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
    public class ChangeUserRoleCommand:IRequest<string>
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

    }
    public class EditRoleValidation:AbstractValidator<ChangeUserRoleCommand>
    {
        public EditRoleValidation() 
        {
            RuleFor(v=>v.RoleId).NotEmpty().WithMessage("Role Id is required");
            RuleFor(v => v.UserId).NotEmpty().WithMessage("Role Name is required");
        }
    }
    public class EditRoleHandler : IRequestHandler<ChangeUserRoleCommand, string>
    {
        private readonly IUnitOfWork _uw;
        public EditRoleHandler(IUnitOfWork uw) 
        { 
            _uw= uw;
        }
 
        public async Task<string> Handle(ChangeUserRoleCommand request, CancellationToken cancellationToken)
        {
            var RoleValidation = new EditRoleValidation();
            var RoleValidationResult = RoleValidation.Validate(request);
            if(RoleValidationResult.IsValid)
            {
                var Result =await _uw.Account.EditRole(request);
                return Result;
            }
            return "Role Namd and Id is required";
        }
    }
}
