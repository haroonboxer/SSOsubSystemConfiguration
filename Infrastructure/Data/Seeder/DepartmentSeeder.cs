using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Seeder
{
    public  class DepartmentSeeder
    {
        public static void Seed(ModelBuilder Build)
        {
            Build.Entity<Department>().HasData(
            new Department { DepartmentId = "1", DepartmentName = "ریاست مخابره" }
            );
        }
    }
}
