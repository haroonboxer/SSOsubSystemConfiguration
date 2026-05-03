using Application.DTO;
using Application.DTO.Accountdto;
using Application.IRepository;
using MediatR;
 

namespace Application.Features.AccountFeatures.Account.Query
{
    public class RolesQuries:IRequest<List<Roledtos>>
    {

    }
    public class RolesqueryHandler : IRequestHandler<RolesQuries, List<Roledtos>>
    {
     private readonly IUnitOfWork _db;
         public RolesqueryHandler(IUnitOfWork db)
         {
                _db = db;
         }

        public Task<List<Roledtos>> Handle(RolesQuries request, CancellationToken cancellationToken)
        {
            var data = _db.Account.LoadRoles();
            return data;

        }
    }
}
