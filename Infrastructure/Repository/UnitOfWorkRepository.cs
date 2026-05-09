using Application.IRepository;
using Infrastructure.Data;
using Infrastructure.Data.Authentications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
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
        private readonly IHttpContextAccessor _httpContext;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpcontext;
        public IAppUser Account { get; private set; }
        public IDepartmentsRepository Departments { get; private set; }
        public ISells sells { get; private set; }
        public UnitOfWorkRepository(AppDbContext db,IHttpContextAccessor httpContext,HttpClient httpClient, IHttpContextAccessor httpcontext) 
        {
            _db = db;
            _httpContext = httpContext;
            _httpClient = httpClient;
            _httpcontext = httpcontext;
            Departments = new DepartmentRepository(_db, _httpClient,_httpcontext);
            Account = new AppUserRepository( _httpContext,_db);
            sells = new Sells(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }

}
