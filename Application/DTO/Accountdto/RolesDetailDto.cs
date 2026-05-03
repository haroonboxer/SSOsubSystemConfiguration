using Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Accountdto
{
    public class RolesDetailDto
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public IList<Claimsdto> Claims { get; set; }

        public List<Claim> RoleClaims { get; set; }
    }
}
