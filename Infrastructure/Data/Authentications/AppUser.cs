using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Authentications
{
    public class AppUser:IdentityUser
    {
        

        public Department? Departments { get; set; }
        public string Department_Id { get; set; }
        public string? Name { get; set; }
        public Rank? Rank { get; set; } // Navigation property
        public int Rankid { get; set; } // Foreign key
        public string UserNameInLocalLang { get; set; }
        public bool? Active { get; set; }
     
        
    }
}
