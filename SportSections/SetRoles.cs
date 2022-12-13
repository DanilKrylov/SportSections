using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace SportSections
{
    public static class SetRoles
    {
        public static async Task InitializeAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var assistentLogin = "assistent";
            var directorLogin = "director";
            string password = "123456";

            if (await roleManager.FindByNameAsync("assistent") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("assistent"));
            }
            if (await roleManager.FindByNameAsync("director") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("director"));
            }


            if (await userManager.FindByNameAsync(assistentLogin) == null)
            {
                IdentityUser assistent = new IdentityUser { UserName = assistentLogin };
                IdentityResult result = await userManager.CreateAsync(assistent, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(assistent, "assistent");
                }
            }


            if (await userManager.FindByNameAsync(directorLogin) == null)
            {
                IdentityUser director = new IdentityUser { UserName = directorLogin };
                IdentityResult result = await userManager.CreateAsync(director, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(director, "director");
                }
            }
        }
    }
}
