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
using Microsoft.AspNetCore.Http;
using Hangfire.Dashboard;

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

        private string FounderSteamId
        {
            get
            {
                return Configuration.GetSection("Site")?["FounderSteamId"] ?? "";
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<Site>(this.Configuration.GetSection("Site"));

            // Add framework services.
            services.AddHangfire(configuration => configuration
				.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));

			services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Founder", policy => policy.RequireClaim("steam:steam:id", this.FounderSteamId));
            });

            // Add application services.
            services.AddTransient<SeedService, SeedService>();
            services.AddTransient<CollectorService, CollectorService>();
            services.AddTransient<TransferService, TransferService>();
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
            }
            else
            {
                app.UseExceptionHandler("/Main/HandleException");
                app.UseStatusCodePagesWithRedirects("/Main/HandleStatusCode/{0}");
            }

			app.UseStaticFiles();

            app.UseIdentity();

			app.UseSteamAuthentication();

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new ClaimsBasedAuthorizationFilter("steam:steam:id", this.FounderSteamId) }
            });
            app.UseHangfireServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Main}/{action=Index}/{id?}");
            });
        }
    }
}
