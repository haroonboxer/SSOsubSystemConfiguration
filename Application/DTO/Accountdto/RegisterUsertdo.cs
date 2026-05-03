using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Accountdto
{
    public class RegisterUsertdo
    {
         public string? Id { get; set; }
        public string Name { get; set; }
      
        public string LocalName { get; set; }
      

        public int RankId { get; set; }
       
        public string RoleId { get; set; }

        
        public string DepartmentId { get; set; }
       
        public string Password { get; set; }
       
        public string ConfirmPassword { get; set; }
        
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        public int ParrentId { get; set; }
        public string UserJob { get; set; }
    }
}
