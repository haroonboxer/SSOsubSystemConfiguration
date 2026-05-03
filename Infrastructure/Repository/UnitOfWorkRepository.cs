using Application.IRepository;
using Infrastructure.Data;
using Infrastructure.Data.Authentications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UnitOfWorkRepository : IUnitOfWork
    {
        private readonly AppDbContext _db;
        //private readonly UserManager<AppUser> _userManager;
        //private readonly SignInManager<AppUser> _signInManager;
        //private readonly RoleManager<IdentityRole> _roleManager;UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,RoleManager<IdentityRole> roleManager,
        private readonly IHttpContextAccessor _httpContext;
        public IAppUser Account { get; private set; }
        public IDepartmentsRepository Departments { get; private set; }
        public ISells sells { get; private set; }
        public UnitOfWorkRepository(AppDbContext db,IHttpContextAccessor httpContext)
        {
            _db = db;
            //_userManager = userManager;
            //_signInManager = signInManager;
            //_roleManager = roleManager;_roleManager,_db,_userManager,_signInManager
            _httpContext = httpContext;
            // add all Repositories Dependencies here 

            Departments = new DepartmentRepository(_db);
            Account = new AppUserRepository( _httpContext);
            sells = new Sells(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }

}
