using BlogProjectOnion.Domain.Entities;
using BlogProjectOnion.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Infrastructure.SeedData
{
    public static class AdminSeedData
    {
        public static async void Seed(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, AppDbContext _context)
        {


            if (!_context.Users.Any(u => u.UserName == "admin"))
            {

                AppUser user = new AppUser
                {
                    UserName = "admin",
                    Email = "eren.colk@gmail.com",
                    EmailConfirmed = true,
                    CreatedDate = DateTime.Now,                   
                    Status = Domain.Enums.Status.Active
                };

                Author author = new Author();
                author.FirstName = "Admin";
                author.Status = Domain.Enums.Status.Active;
                author.CreatedDate = DateTime.Now;
                await _context.Authors.AddAsync(author);
                await _context.SaveChangesAsync();
                user.Author = author;
                userManager.CreateAsync(user, "Eren12345.").Wait();

                AppUser createdUser = await userManager.FindByNameAsync("admin");

                AppRole adminRole = await roleManager.FindByNameAsync("Admin");
                await userManager.AddToRoleAsync(createdUser, adminRole.Name);
            }
        }
    }
}
