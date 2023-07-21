using Learn_core_mvc.Core.Models;
using Learn_core_mvc.Filters;
using Learn_core_mvc.IdentityDbContextFolder;
using Learn_core_mvc.Models;
using Learn_core_mvc.Repository;
using Learn_core_mvc.Repository.EFDBFirstRepo;
using Learn_core_mvc.Service;
using Learn_core_mvc.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Learn_core_mvc
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
            services.AddControllersWithViews();

            services.AddSession();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/ClaimBaseAuth/LogIn";
                options.LogoutPath = "/ClaimBaseAuth/LogOff";
                options.AccessDeniedPath = "/Home/AccessDenied";
            });

            // For Identity
            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(this.Configuration.GetConnectionString("MyConnectionString"));
            });

            // For Identity
            services.AddIdentity<IdentityUser, IdentityRole>
            (options =>
            {
                options.SignIn.RequireConfirmedAccount = true; // For email confirmation
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1); // Block user temporarily on wrong password attempt
                options.Lockout.MaxFailedAccessAttempts = 3; // Block user temporarily on wrong password attempt
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            // If unauthenticated user, access authorize resource then 
            services.ConfigureApplicationCookie(config=> {
                config.LoginPath = "/IdentityEx/Login";
            });

            // To get logged-in userid using identity, this is a custom servies
            services.AddScoped<IIdentityUserService, IdentityUserService>();

            services.AddScoped<ActionEncodeFilter>();

            //services.AddScoped<ISampleSqlRepository<Employee>, SampleSqlRepository<Employee>>((ctx) =>
            //{
            //    return new SampleSqlRepository<Employee>(
            //        this.Configuration.GetConnectionString("MyConnectionString")
            //    );
            //});

            //services.AddScoped<ISampleSqlRepository<Company>, SampleSqlRepository<Company>>((ctx) =>
            //{
            //    return new SampleSqlRepository<Company>(
            //        this.Configuration.GetConnectionString("MyConnectionString")
            //    );
            //});

            services.AddScoped<ISampleSqlRepository, SampleSqlRepository>((ctx) =>
            {
                return new SampleSqlRepository(
                    this.Configuration.GetConnectionString("MyConnectionString")
                );
            });
            services.AddScoped<IEFCoreDBFirstRepository, EFCoreDBFirstRepository>();

            services.AddScoped<ISampleService, SampleService>();
            services.AddScoped<IEFCoreDBFirstService, EFCoreDBFirstService>();

            services.AddScoped<IEmailService, EmailService>();

            services.Configure<SMTPConfigModel>(this.Configuration.GetSection("SMTPConfig"));

            services.Configure<InfoObjConfig>(this.Configuration.GetSection("infoObj"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/Home/Error");
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                                    Path.Combine(Directory.GetCurrentDirectory(), "TestStaticFiles")
                                ),
                RequestPath = "/TestStaticFiles"
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                //For handling unwanted route.
                endpoints.MapControllerRoute(
                    name: "cms",
                    pattern: "{**route}",
                    new { Controller = "Content", Action = "DynamicRoute" });
            });

            RotativaConfiguration.Setup(env.WebRootPath, "Rotativa");
        }
    }
}
