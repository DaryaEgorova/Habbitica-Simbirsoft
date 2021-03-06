using System.Data.SqlClient;
using Habbitica.BLL_DAL.EF;
using Habbitica.BLL_DAL.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Habbitica.BLL_DAL.DBfunctions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;
using Microsoft.AspNetCore.Identity;
using Habbitica.BLL_DAL.Interfaces;
using Habbitica.BLL_DAL.Repositories;
using Habbitica.BLL_DAL.Interfaces;
using Habbitica.BLL_DAL.Services;
using Habbitica.BLL_DAL.Entity;
using Maganizer_Project.DAL.Repositories;
using MySql.Data.MySqlClient;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;

namespace Habbitica
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication()
                .AddFacebook(options =>
                {
                    options.AppId = "866414814286929";
                    options.AppSecret = "ff49d08b76e9cfad140b5667424346f7";
                });
            services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddSession();

            services.AddSignalR();

            services.AddRazorPages();

            services.AddDbContext<HabbiticaContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("localhost")));


            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<HabbiticaContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            });

            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = Configuration["Application:LoginPath"];
            });

            var mailKitOptions = Configuration.GetSection("Email").Get<MailKitOptions>();
            services.AddMailKit(config => { config.UseMailKit(mailKitOptions); });
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
            services.AddScoped<IAccountService, UserAccountService>();
            services.AddTransient<IUserProfileService, UserProfileService>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/missing";
                    await next();
                }
            });
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
            });
        }
    }
}