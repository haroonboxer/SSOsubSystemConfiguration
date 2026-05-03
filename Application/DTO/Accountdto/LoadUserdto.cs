using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Accountdto
{
    public class LoadUserdto
    {
       
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
        public List<UserListDto> data { get; set; }

    }
}
