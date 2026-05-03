using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Accountdto
{
    public class ChangeUserRoledto
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}
