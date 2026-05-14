using Application.DTO.Accountdto;
using Application.DTO.Departmentdto;
using Application.IRepository;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Net.Http.Json;
namespace Infrastructure.Repository
{
    public class DepartmentRepository : IDepartmentsRepository
    {
       
        private readonly AppDbContext _db;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpcontext;
        public DepartmentRepository(AppDbContext db,HttpClient httClient, IHttpContextAccessor httpcontext)  
        {
            _db = db;
            _httpClient = httClient; 
            _httpcontext = httpcontext;
        }
        public async Task<DataTableReturnType> LoadDepartment(int start,int length,string BaseURL)
        {
            var Department = await _db.Database.SqlQuery<Application.DTO.Departmentdto.Departmentdto>($@"SELECT [DepartmentId],[DepartmentName],[AddedBy] FROM [Departments]")
                .Skip(start)
                .Take(length)
                .ToListAsync();
            var userIds = Department.Select(x => x.AddedBy).Distinct().ToList();

            var token = _httpcontext.HttpContext.Request.Cookies["access_token"];
            _httpClient.DefaultRequestHeaders.Authorization =    new AuthenticationHeaderValue("Bearer", token);
      
        

            var response = await _httpClient.PostAsJsonAsync(
                      $"{BaseURL}/Account/LoadUserName",
                      userIds
                  );

            var users = await response.Content.ReadFromJsonAsync<List<UserDto>>();
            var userDict = users.ToDictionary(x => x.UserId, x => x.UserName);

            foreach (var item in Department)
            {
                item.AddedBy = userDict.ContainsKey(item.AddedBy)
                    ? userDict[item.AddedBy]: "Unknown";
            }
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
