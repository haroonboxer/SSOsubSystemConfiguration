using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Seeder
{
    public static class SeedingClass
    {
        public static void Seeding(ModelBuilder Build)
        {
            //AppUserseeder.seed(Build);
            RankTypeSeeders.seed(Build);
            RankSeeder.Seed(Build);
            DepartmentSeeder.Seed(Build);
            RoleSeeder.Seed(Build);
            //UserRolesSeeder.seed(Build); 
            AllClaimsSeeder.seed(Build);
        }
    }
}
