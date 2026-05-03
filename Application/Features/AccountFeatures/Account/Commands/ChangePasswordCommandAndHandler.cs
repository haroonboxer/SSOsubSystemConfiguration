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
    public class ChangePasswordCommand:IRequest<string>
    {
        public string UserId { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
        
    }
    public class ChangePasswordValidation:AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidation()
        {
            RuleFor(v => v.UserId).NotEmpty().WithMessage("User id is required");
            RuleFor(v => v.password).NotEmpty().WithMessage("Password is required");
            RuleFor(v => v.confirmPassword).NotEmpty().WithMessage("confirm password is required").Equal(v => v.password).WithMessage("password and confirm password need to be same"); ;
            

        }
    }
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand,string>
    {
        private readonly IUnitOfWork _uw;
        

        public ChangePasswordHandler(IUnitOfWork uw)
        {
            _uw = uw;
        }
 

        public async Task<string> Handle(ChangePasswordCommand request,CancellationToken cancellationtoken)
        {
            var ChangePasswordValidator = new ChangePasswordValidation();
            var changepasswordvalidatorResult = ChangePasswordValidator.Validate(request);
            if(changepasswordvalidatorResult.IsValid)
            {
                var ChangePasswrodresult = await _uw.Account.ChangePassword(request);
                return ChangePasswrodresult;
            }
            return string.Join(", ", changepasswordvalidatorResult.Errors.Select(e => e.ErrorMessage));
        }
    }
}
