using Infrastructure.Data.Authentications;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Seeder
{
    public class RoleSeeder
    {
        public static void Seed(ModelBuilder Build)
        {
            Build.Entity<Roles>().HasData(
                new Roles { Id = "ac976318-0f8a-4a98-81e1-c855bc496cbd", Name = "Super Admin", NormalizedName = "SUPER ADMIN" });
        }
    }
}
