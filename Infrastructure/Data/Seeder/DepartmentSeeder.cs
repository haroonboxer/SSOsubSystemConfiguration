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
            new Department { DepartmentId = "1", DepartmentName = "ریاست مخابره",AddedBy= "8498d607-189d-456b-a5ef-7833734fac92",EditBy= "8498d607-189d-456b-a5ef-7833734fac92" }
            );
        }
    }
}
