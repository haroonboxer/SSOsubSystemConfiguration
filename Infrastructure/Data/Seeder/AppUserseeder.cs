using Domain;
using Infrastructure.Data.Authentications;
using MediatR.NotificationPublishers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Seeder
{
    public class AppUserseeder
    {
        public static async Task Seed(IServiceProvider service)
        {
            var userManager = service.GetRequiredService<UserManager<AppUser>>();
            var user = await userManager.FindByNameAsync("Haroon");
            if (user == null)
            {
                var data = new AppUser
                {
                     UserName = "Haroon",
                     Email = "Haroo@gmail.com",
                     Name = "Haroon",
                     Rankid = 1,
                     Department_Id = "1",
                     Active = true,
                     UserNameInLocalLang = "هارون"
                };  
                await userManager.CreateAsync(data);


                await userManager.AddPasswordAsync(data,"Test@123");
                await userManager.AddToRoleAsync(data, "Super Admin");
            }
            var Saveduser = await userManager.FindByNameAsync("Haroon");
            var UserRole = await userManager.GetRolesAsync(Saveduser);
            if(UserRole.Count ==0)
            {
                await userManager.AddToRoleAsync(Saveduser, "Super Admin");

                await userManager.AddPasswordAsync(Saveduser, "Test@123");
                await userManager.AddToRoleAsync(Saveduser, "Super Admin");
            }
           
  
            var AddRoleToUser = await userManager.FindByNameAsync("Haroon");
            if(string.IsNullOrEmpty(AddRoleToUser.PasswordHash))
            {
                var result =  await userManager.AddPasswordAsync(AddRoleToUser,"Test@12345");
            }
            if (UserRole.Count ==0)
            {
                await userManager.AddToRoleAsync(AddRoleToUser, "Super Admin");

            }
      
            
        }

    }
}