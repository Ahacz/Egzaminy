using Egzaminy.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Egzaminy.Startup))]
namespace Egzaminy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            ConfigureAuth(app);
            DodajRoleIAdminaAsync();
            
        }
        private async System.Threading.Tasks.Task DodajRoleIAdminaAsync()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            string[] role = { "Admin", "Wykładowca", "Student" };
            foreach (var rola in role)
            {
                var istnieje = await RoleManager.RoleExistsAsync(rola);
                if (!istnieje)
                {
                    var nowa = new IdentityRole();
                    nowa.Name = rola;
                    RoleManager.Create(nowa);
                }
            }
            System.Threading.Tasks.Task<ApplicationUser> jestadm = UserManager.FindByEmailAsync("admin@test.pl");
            jestadm.Wait();
            if (jestadm.Result == null)
            {
                ApplicationUser admin = new ApplicationUser();
                admin.Email = "admin@test.pl";
                admin.UserName = "admin";
                admin.Imie = "Michał";
                admin.Nazwisko = "Mazurkiewicz";

                System.Threading.Tasks.Task<IdentityResult> dodaj = UserManager.CreateAsync(admin, "lolki5");
                dodaj.Wait();
                if (dodaj.Result.Succeeded)
                {
                    System.Threading.Tasks.Task<IdentityResult> nowarola = UserManager.AddToRoleAsync(admin.Id, "Admin");
                    nowarola.Wait();
                }
            }
        }
    }
}
