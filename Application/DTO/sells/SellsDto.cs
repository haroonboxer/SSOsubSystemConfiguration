using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.sells
{
    public class SellsDto
    {
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public int? draw { get; set; }
        public List<SellsList> data { get; set; }
    }
}
