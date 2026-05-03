using Application.DTO.sells;
using Application.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Sells.Query
{
    public class sellsQeury:IRequest<SellsDto>
    {
        public int start { get; set; }
        public int length { get; set; }

    }
    public class SellsHandle:IRequestHandler<sellsQeury,SellsDto>
    {
        private readonly IUnitOfWork _db;
        public SellsHandle(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task<SellsDto> Handle(sellsQeury request, CancellationToken cancellationtoken)
        {
            var data = await _db.sells.LoadSells(request.start,request.length);
            
            return data;
        }
    }
}
