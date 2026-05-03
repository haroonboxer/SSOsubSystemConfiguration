using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Authentications
{
    public class Roles:IdentityRole
    {
        public virtual ICollection<RoleClaims> Claims { get; set; }
    }
}
