using Application.IRepository;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AccountFeatures.Account.Commands
{
    public class RegisterationCommand:IRequest<string>
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string LocalName { get; set; }
        public int RankId { get; set; }
        public string RoleId { get; set; }
        public string DepartmentId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class RegistrationValidation : AbstractValidator<RegisterationCommand>
    {
      public RegistrationValidation()
        {
            RuleFor(v=>v.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(v => v.LocalName).NotEmpty().WithMessage("pashto name is required");
            RuleFor(v => v.RankId).NotEmpty().WithMessage("Rank is required");
            RuleFor(v=>v.PhoneNumber).NotEmpty().WithMessage("Phone number is required");
            RuleFor(v => v.RoleId).NotEmpty().WithMessage("Role is required");
            RuleFor(v => v.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(v => v.DepartmentId).NotEmpty().WithMessage("Department is required");
        }
    }

    public class RegisterationHandler : IRequestHandler<RegisterationCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RegisterationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(RegisterationCommand request, CancellationToken cancellationToken)
        {
          var resutl =   await _unitOfWork.Account.RegisterUser(request);
            return resutl;
        }
    }



}
