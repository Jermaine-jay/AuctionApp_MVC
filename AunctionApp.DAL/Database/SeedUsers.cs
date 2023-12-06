using AunctionApp.DAL.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace AunctionApp.DAL.Database
{
    public static class SeedUsers
    {
        public static async Task EnsurePopulatedUsersAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();


                if (!await userManager.Users.AnyAsync())
                {
                    foreach (User user in GetUsers())
                    {
                        await userManager.CreateAsync(user, user.PasswordHash);
                    }
                }

                User user1 = await userManager.FindByNameAsync("Jota10");
                User user2 = await userManager.FindByNameAsync("Jermaine10");
                User user3 = await userManager.FindByNameAsync("Idan10");

                if (user1 != null)
                {
                    await userManager.AddToRoleAsync(user1, "User");
                }

                if (user2 != null)
                {
                    await userManager.AddToRolesAsync(user2, new[] { "Admin", "SuperAdmin" });
                }

                if (user3 != null)
                {
                    await userManager.AddToRolesAsync(user3, new[] { "Admin" });
                }
            }
        }


        private static IEnumerable<User> GetUsers()
        {
            return new List<User>()
            {
                new User
                {
                    UserName = "Jota10",
                    FirstName = "Jota",
                    LastName = "Diogo",
                    Email = "jermaine.jay00@gmail.com",
                    PhoneNumber = "1234567890",
                    Address = "Centenary City Enugu Nigeria",
                    PasswordHash = "12345qwert",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                },

                 new User
                {
                    UserName = "Idan10",
                    FirstName = "Mo",
                    LastName = "Salah",
                    Email = "jsonosita@outlook.com",
                    PhoneNumber = "1334447880",
                    Address = "Centenary City Enugu Nigeria",
                    PasswordHash = "12345qwert",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                },

                new User
                {
                    UserName = "Jermaine10",
                    FirstName = "Roberto",
                    LastName = "Firmino",
                    Email = "jsonosii097@gmail.com",
                    PhoneNumber = "1234447890",
                    Address = "Centenary City Enugu Nigeria",
                    PasswordHash = "12345qwert",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                }
            };
        }
    }
}

