using Application.DTO.sells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepository
{
    public interface ISells
    {
        public Task<SellsDto> LoadSells(int start, int length);
    }
}
