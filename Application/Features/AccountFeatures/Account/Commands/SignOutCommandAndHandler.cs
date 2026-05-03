using Application.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AccountFeatures.Account.Commands
{
    public class SignOutCommand:IRequest<string>
    {
    }
    public class SignOutHandler:IRequestHandler<SignOutCommand,string>
    {
        private readonly IUnitOfWork _uw;
        public SignOutHandler(IUnitOfWork uw)
        {
            _uw = uw;
        }
        public async Task<string>Handle(SignOutCommand request,CancellationToken cancellationtoken)
        {
            var Result =await _uw.Account.Logout();
            return  Result; 
        }
    }
}
