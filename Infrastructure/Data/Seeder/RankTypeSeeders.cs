using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Seeder
{
    public  class RankTypeSeeders
    {
        public static void seed(ModelBuilder Build)
        {
            Build.Entity<RankType>().HasData(
                new RankType { Id = 1, Name = "نظامی" },
                new RankType { Id = 2, Name = "ملکی" },
                new RankType { Id = 3, Name = "قراردادی" },
                new RankType { Id = 4, Name = "بالمقطع" }
                );
        }
    }
}
