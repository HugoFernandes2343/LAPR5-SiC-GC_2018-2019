using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SiC.Models;
using Microsoft.EntityFrameworkCore;

namespace SiC
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //add Cors to this API DEPLOYMENT MODE
            services.AddCors(opt =>
            {
                opt.AddPolicy("CORS",
                b => b.WithOrigins("https://lapr5-ui.herokuapp.com")
                .WithOrigins("https://lapr5-enc.herokuapp.com")
                .AllowAnyHeader()
                .AllowAnyMethod());
            }
            );

            //add Cors to this API DEVELOPMENT MODE
            /*services.AddCors(opt =>
            {
                opt.AddPolicy("CORS",
                b => b.WithOrigins("http://localhost:4200")
                .WithOrigins("http://localhost:8080")
                .AllowAnyHeader()
                .AllowAnyMethod());
            }
            );*/

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<SiCContext>(options => options.UseLazyLoadingProxies()
                    .UseSqlServer(Configuration.GetConnectionString("SiCContextDeployment")));
                    //.UseSqlServer(Configuration.GetConnectionString("SiCContextDevelopment")));


            //var connection = "Data Source=catalog.db";
            //    services.AddDbContext<CatalogContext>
            //    (options => options.UseSqlite(connection));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseCors("CORS");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
