using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AllClaim
    {
            public int Id { get; set; }
            public string ClaimType { get; set; }
            public bool ClaimValue { get; set; }
            public string groupClaims { get; set; }
    }
}
