using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Seeder
{
    public class AllClaimsSeeder
    {
        public static void seed(ModelBuilder build)
        {
            build.Entity<AllClaim>().HasData(
                new AllClaim {Id=1,ClaimType="Authentication Management",ClaimValue=true,groupClaims="Authentication"},
                new AllClaim { Id=2,ClaimType = "Add User",ClaimValue=true,groupClaims = "Authentication" },
                new AllClaim { Id=3,ClaimType = "Edit User",ClaimValue = true,groupClaims= "Authentication" },
                new AllClaim { Id=4 ,ClaimType = "User Activation", ClaimValue = true,groupClaims= "Authentication" },
                new AllClaim { Id=5,ClaimType = "Add Role",ClaimValue= true,groupClaims = "Authentication" },
                new AllClaim { Id=6,ClaimType = "Edit Role",ClaimValue = true,groupClaims= "Authentication" },
                new AllClaim { Id = 7,ClaimType = "Role Management",ClaimValue=true,groupClaims= "Authentication" }
                );
        }
    }
}
