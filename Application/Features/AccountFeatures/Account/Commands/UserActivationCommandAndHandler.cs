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
    public class UserActivationCommand : IRequest<string>
    {
        public string UserId { get; set; }
        public bool IsActive { get; set; }

    }
    public class UserActivationValidator:AbstractValidator<UserActivationCommand>
    {
        public UserActivationValidator() 
        {
           
            RuleFor(v => v.UserId).NotEmpty().WithMessage("user id is required");
        }
    }
    public class UserActivationHandler : IRequestHandler<UserActivationCommand, string>
    {
        private readonly IUnitOfWork _uw;

        public UserActivationHandler(IUnitOfWork uw)
        {
            _uw = uw;
        }
        public async Task<string> Handle(UserActivationCommand request, CancellationToken cancellationToken)
        {
            var UserValidator = new UserActivationValidator();
            var ValidationResult = UserValidator.Validate(request);
            if (ValidationResult.IsValid)
            {
                var result = await _uw.Account.ActivateDeActivatedUser(request.UserId,request.IsActive);
                return result;
            }

            return  "No data found";
        }
    }


}
