using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Mvc.Routing;
using Infrastructure.Data.Seeder;
using Infrastructure.Data.Authentications;


namespace Infrastructure.Data
{
    public class AppDbContext: IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var Departments = builder.Entity<Department>();
            Departments.HasKey(e => e.DepartmentId);
            var Rank = builder.Entity<Rank>();
            Rank.HasKey(x => x.Id);
            Rank.HasOne(e => e.RankTypeIds).WithMany().HasForeignKey(e=>e.RankTypeId);

            var AppUser = builder.Entity<AppUser>();
            AppUser.HasOne(e => e.Rank).WithMany().HasForeignKey(e =>e.Rankid).OnDelete(DeleteBehavior.NoAction);
            AppUser.HasOne(e => e.Departments).WithMany().HasForeignKey(e=>e.Department_Id);


            builder.Entity<Roles>()
              .HasMany(r => r.Claims)
              .WithOne()
              .HasForeignKey(c => c.RoleId)
              .IsRequired();

            SeedingClass.Seeding(builder);



        }

        public DbSet<Rank>ranks { get; set; }
        public DbSet<RankType> rankTypes { get; set; }
        public DbSet<AppUser> appUsers { get; set; }
        public DbSet<Department> Departments { get;set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<AllClaim> AllClaims { get; set; }
        public DbSet<RoleClaims> RoleClaims { get; set; }
        public DbSet<Sells> sells { get; set; }


    }
}
