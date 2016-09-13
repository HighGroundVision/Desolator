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
using HGV.AD.Web.Data;
using HGV.AD.Web.Models;
using HGV.AD.Web.Services;
using Hangfire;
using HGV.AD.Web.Configuration;

namespace HGV.AD.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

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
            services.AddOptions();
            services.Configure<Site>(Configuration.GetSection("Site"));

            // Add framework services.
            services.AddHangfire(configuration => configuration
				.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));

			services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            //services.AddTransient<IEmailSender, AuthMessageSender>();
            //services.AddTransient<ISmsSender, AuthMessageSender>();
			services.AddTransient<SeedService, SeedService>();
            services.AddTransient<CollectorService, CollectorService>();
            services.AddTransient<TransferService, TransferService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            /*
            if (env.IsDevelopment())
            {
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
				// app.UseBrowserLink();
			}
            else
            {
				app.UseExceptionHandler("/Main/HandleException");
                app.UseStatusCodePagesWithRedirects("/Main/HandleStatusCode/{0}");
            }
            */

            app.UseDeveloperExceptionPage();
            app.UseDatabaseErrorPage();
            
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[]
                {
                    new HGVHangFireAuthorizationFilter()
                }
            });

			app.UseHangfireServer();

			app.UseStaticFiles();

            app.UseIdentity();

			app.UseSteamAuthentication();

			app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Main}/{action=Index}/{id?}");
            });
        }
    }
}
