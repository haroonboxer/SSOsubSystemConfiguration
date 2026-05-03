using Application.DTO.Accountdto;
using Application.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AccountFeatures.Account.Query
{

    public class LoginCommand:IRequest<string>
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
    public class LongHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IUnitOfWork _db;
        public LongHandler(IUnitOfWork db)
        {
            _db = db;
        }
        public Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var data = new Logindto
            {
                Name = request.Name,
                Password = request.Password,
            };
             var resutl  = _db.Account.Login(data);
            return resutl;
        }
    }
}
