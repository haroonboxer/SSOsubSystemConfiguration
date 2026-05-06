 
using Application.DTO.Departmentdto;
using Application.IRepository;
using Domain;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class DepartmentRepository : IDepartmentsRepository
    {
       
        private readonly AppDbContext _db;
        public DepartmentRepository(AppDbContext db)  
        {
            _db = db;
        }
        public async Task<DataTableReturnType> LoadDepartment(int start,int length)
        {
            var Department = await _db.Database.SqlQuery<Departmentdto>($@"SELECT [DepartmentId],[DepartmentName],[AddedBy] FROM [Departments]")
                .Skip(start)
                .Take(length)
                .ToListAsync();
            var DepartmentCount = await _db.Departments.CountAsync();
            var DepartmentCurrentCount = Department.Count();
            if (Department != null)
            {
                var data = new DataTableReturnType
                {
                    data = Department,
                    recordsTotal = DepartmentCount,
                    recordsFiltered = DepartmentCurrentCount
                };
                return data;
            }
            else
            {
                return null;
            }
        }

    }
}
