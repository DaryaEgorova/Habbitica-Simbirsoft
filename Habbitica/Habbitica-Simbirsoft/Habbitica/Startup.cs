using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        }

     
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
                app.UseExceptionHandler("/Error");
               
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
    
    
}
