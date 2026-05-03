using Application.DTO.Accountdto;
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
    public class AddRoleCommand:IRequest<string>
    {
        public string RoleName { get; set; }
    }
    public class RoleValidation : AbstractValidator<AddRoleCommand>
    {
        public RoleValidation()
        {
            RuleFor(v=>v.RoleName).NotEmpty().WithMessage("Role Name is required");
        }
    }

    public class AddRoleHandler : IRequestHandler<AddRoleCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddRoleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var Validator = new RoleValidation();
            var validationResult = Validator.Validate(request);
            if(!validationResult.IsValid)
            {
                var error = string.Join("<br/>",
                    validationResult.Errors.Select(e => e.ErrorMessage));
                return error;
            }
            var newdata = new Roledtos
            {
                RoleName = request.RoleName
            };

            var result = await _unitOfWork.Account.AddRole(newdata);
            return result;
        }
    }

}
