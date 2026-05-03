using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Departmentdto
{
    public  class DataTableReturnType
    {
        public int? draw { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
        public List<Departmentdto> data { get; set; }

    }
}
