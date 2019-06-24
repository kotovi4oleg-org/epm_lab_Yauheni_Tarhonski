using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TinyERP4Fun.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI.Services;
using TinyERP4Fun.Scheduler;
using TinyERP4Fun.Controllers;
using TinyERP4Fun.Interfaces;
using TinyERP4Fun.ModelServises;

namespace TinyERP4Fun
{
    public class Startup
    {
        public class EmailSender : IEmailSender
        {
            public Task SendEmailAsync(string email, string subject, string htmlMessage)
            {
                return Task.CompletedTask;
            }
        }
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


            services.AddDbContext<DefaultContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                //AddDefaultIdentity<IdentityUser>()
                //.AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<DefaultContext>()
                .AddDefaultTokenProviders();

            services.AddSingleton<IEmailSender, EmailSender>();

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            //Добавляем шедулер
            services.AddHostedService<TimedHostedServiceUpdateCurrencies>();

            //DI
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IWarehouseService, WarehouseService>();
            services.AddScoped<IUnitService, UnitService>();
            services.AddScoped<IExpencesService, ExpencesService>();
            services.AddScoped<IBusinessDirectionsService, BusinessDirectionsService>();
            services.AddScoped<ICostItemsService, CostItemsService>();
            services.AddScoped<IDocumentTypesService, DocumentTypesService>();

            services.AddScoped<ICurrencyRatesService, CurrencyRatesService>();
            services.AddScoped<ICitiesService, CitiesService>();
            services.AddScoped<ICommunicationsService, CommunicationsService>();
            services.AddScoped<ICommunicationTypesService, CommunicationTypesService>();
            services.AddScoped<ICompaniesService, CompaniesService>();
            services.AddScoped<ICountriesService, CountriesService>();
            services.AddScoped<ICurrenciesService, CurrenciesService>();
            services.AddScoped<ICurrencyRatesService, CurrencyRatesService>();
            services.AddScoped<IDepartmentsService, DepartmentsService>();

            services.AddScoped<IEmployeesService, EmployeesService>();
            services.AddScoped<IPeopleService, PeopleService>();
            services.AddScoped<IPositionsService, PositionsService>();
            services.AddScoped<IStatesService, StatesService>();
            services.AddScoped<IUpdateCurrencyRatesService, UpdateCurrencyRatesService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseStatusCodePages();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
