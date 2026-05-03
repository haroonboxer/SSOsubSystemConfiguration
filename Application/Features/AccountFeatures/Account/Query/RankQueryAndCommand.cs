using Application.DTO.Accountdto;
using Application.IRepository;
using MediatR;

namespace Application.Features.AccountFeatures.Account.Query
{
    public class RankQuery:IRequest<List<LoadRankdto>>
    {
    }
    public class RankHandler : IRequestHandler<RankQuery, List<LoadRankdto>>
    {
        private readonly IUnitOfWork _db;
        public RankHandler(IUnitOfWork db)
        {
            _db = db;
        }
        public async Task<List<LoadRankdto>> Handle(RankQuery request, CancellationToken cancellationToken)
        {
            var data = await _db.Account.LoadRank();
            return data;
        }
    }
}
