using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AunctionApp.DAL.Database
{
    public static  class SeedRoles
    {
        public static async Task SeedRole(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                AunctionAppDbContext context = scope.ServiceProvider
                    .GetRequiredService<AunctionAppDbContext>();

                context.Database.EnsureCreated();
                var roleExist = context.Roles.Any();

                if (!roleExist)
                {
                    context.Roles.AddRange(await SeededRoles());
                    await context.SaveChangesAsync();
                }
            }
        }


        private static async Task<IEnumerable<IdentityRole>> SeededRoles()
        {
            return new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole()
                {
                   Name = "User",
                   NormalizedName = "USER"
                                                 
                },
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN"
                }
            };
        }
    }
}
