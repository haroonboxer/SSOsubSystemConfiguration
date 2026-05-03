using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Rank
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int RankTypeId { get; set; }
 
        public RankType RankTypeIds { get; set; }
    }
}
