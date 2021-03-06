﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.StaticFiles;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Windays2016.EnvironmentAndStartup.Models;
using Windays2016.EnvironmentAndStartup.Services;

namespace Windays2016.EnvironmentAndStartup
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddCaching(); // Adds a default in-memory implementation of IDistributedCache
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.CookieName = ".MyApplication";      // default .AspNet.Session
            });


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

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
                try
                {
                    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                        .CreateScope())
                    {
                        serviceScope.ServiceProvider.GetService<ApplicationDbContext>()
                             .Database.Migrate();
                    }
                }
                catch { }
            }

            app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseIdentity();

            // To configure external authentication please see http://go.microsoft.com/fwlink/?LinkID=532715

            // OBAVEZNO: Staviti prije UseMvc()!
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}








//var currentWindowsUser = System.Security.Principal.WindowsIdentity.GetCurrent()?.Name;
//            if (currentWindowsUser != null)
//            {
//                currentWindowsUser = currentWindowsUser.Substring(currentWindowsUser.LastIndexOf(@"\", StringComparison.Ordinal) + 1);
//                builder.AddJsonFile($"appsettings.{currentWindowsUser}.json", optional: true);
//            }
























//app.Use(async (context, next) =>
//            {
//                await context.Response.WriteAsync($"- Prvi app.Use(){Environment.NewLine}");

//                await next();

//                await context.Response.WriteAsync($"- Nakon prvog app.Use(){Environment.NewLine}");
//            });

//app.Run(async context =>
//{
//    await context.Response.WriteAsync($"-- Prvi app.Run(){Environment.NewLine}");
//});

//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync($"- Drugi app.Use(){Environment.NewLine}");

//    await next();

//    await context.Response.WriteAsync($"- Nakon drugog app.Use(){Environment.NewLine}");
//});
























//// style-src, img-src, script-src, font-src, form-action...
//// 'self', 'none', 'unsafe-inline', 'unsafe-eval', url*
//app.Use(async (ctx, next) => {
//    ctx.Response.Headers.Add("Content-Security-Policy",
//                                "style-src 'none'; report-uri /cspreport");
//    await next();
//});
























//app.UseStaticFiles(new StaticFileOptions()
//{
//    FileProvider = new PhysicalFileProvider(@"d:\Development\Presentations\2016 WinDays\Windays2016.StaticFiles"),
//    RequestPath = "/webfiles"
//});


















//services.AddCaching(); // Adds a default in-memory implementation of IDistributedCache
//services.AddSession(options => {
//    options.IdleTimeout = TimeSpan.FromMinutes(30);
//    options.CookieName = ".MyApplication";      // default .AspNet.Session
//});




//// OBAVEZNO: Staviti prije UseMvc()!
//app.UseSession();

