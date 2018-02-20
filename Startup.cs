using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using KrisnaldoApp.Data;
using KrisnaldoApp.Models;
using KrisnaldoApp.Services;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;


namespace KrisnaldoApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json");
            //.AddJsonFile("appsettings.smarterasp.json", optional: true, reloadOnChange: true);
            //.AddJsonFile("appsettings.azure.json", optional: true, reloadOnChange: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            string Local = "Server=(localdb)\\mssqllocaldb;Database=aspnet-KrisnaldoApp-009a51e2-478c-4020-ac03-0a98477155a1;Trusted_Connection=True;MultipleActiveResultSets=true;";
            //string Azure = "Data Source=tcp:krisnaldoserver.database.windows.net,1433;Initial Catalog=KrisnaldoDB;User Id=adminuser@krisnaldoserver.database.windows.net;Password=Abc123**;";
            string SmarterASP = "Data Source=SQL5028.SmarterASP.NET;Initial Catalog=DB_A0576A_JoachimAlly9;User Id=DB_A0576A_JoachimAlly9_admin;Password=Brc5_4h7;";
            // Add framework services.
            services.AddEntityFramework()
                .AddEntityFrameworkSqlServer()
                .AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(SmarterASP));

            services.AddDirectoryBrowser();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images")),
                RequestPath = new PathString("/images")
            });
            app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\seed")),
                RequestPath = new PathString("/seed")
            });

            app.UseIdentity();

           

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Seizoenen",
                    template: "Seizoenen/CompetitieStatistieken/{*seizoennaam}",
                    defaults: new { controller = "Seizoenen", action = "CompetitieStatistieken" }
                    );

                /*Historical re-route*/
                routes.MapRoute(
                    name: "Albums",
                    template: "Albums/Details/{*albumnaam}",
                    defaults: new { controller = "Album", action = "Details" }
                );

                /*Better re-route*/
                routes.MapRoute(
                    name: "AlbumsBetterReroute",
                    template: "Albums/{*albumnaam}",
                    defaults: new { controller = "Album", action = "Details" }
                );

                routes.MapRoute(
                    name: "SpelerReRouteByName",
                    template: "Spelers/{*spelernaam}",
                    defaults: new { controller = "Spelers", action = "Details" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                    );
                routes.MapRoute(
                    name: "SponsorReRoute",
                    template: "Sponsors",
                    defaults: new { controller = "Sponsor", action = "Index" }
                    );

                routes.MapRoute(
                    name: "AlbumReRoute",
                    template: "Albums",
                    defaults: new { controller = "Album", action = "Index" }
                    );

            });



            

           // SeedData.Initialize(app.ApplicationServices,env);
       } 
    }
}
