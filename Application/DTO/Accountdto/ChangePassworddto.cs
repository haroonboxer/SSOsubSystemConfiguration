using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Accountdto
{
    public class ChangePassworddto
    {
        public string UserId { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
    }
}
