using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Stripe;
using TraincelAPI.Models.VM;
using TraincelAPI.Repository;
using TraincelAPI.Repository.Interface;
using TraincelAPI.Services;
using TraincelAPI.Services.Interface;
using TraincelAPI.Utilities;

namespace TraincelAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "TraincelAPI", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddCors((options) =>
            {
                options.AddPolicy(MyAllowSpecificOrigins, builder =>
                {
                    builder.WithOrigins("localhost:4200",
                        "http://localhost:4200",
                        "https://traincel.azurewebsites.net",
                        "http://traincel.azurewebsites.net",
                        "http://traincel.com",
                        "https://traincel.com",
                        "http://www.traincel.com",
                        "https://www.traincel.com")
                   
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
            services.AddDbContext<Models.DB.TraincelContext>((options) => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddAutoMapper(c => c.AddProfile<AutoMapping>(), typeof(Startup));
            services.Configure<MyAzureBlobConfig>(Configuration.GetSection("MyAzureBlobConfig"));
            AddServices(services);
            AddRepository(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TraincelAPI");
                c.InjectJavascript("/swagger/custom.js");
            });
        }

        private void AddServices(IServiceCollection services)
        {
            //services.AddScoped<IWebinarRepository, WebinarRepository>();
            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<ICountriesService, CountriesService>();
            services.AddScoped<IFacultiesService, FacultiesService>();
            services.AddScoped<IPurchasedOptionsService, PurchaseOptionsService>();
            services.AddScoped<IWebinarService, WebinarService>();
            services.AddScoped<IOrdersAndInvoicesService, OrderAndInvoiceService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICompaniesService, CompaniesService>();
            services.AddScoped<IS3Service, S3Service>();
            services.AddScoped<IEmailService, EmailService>();


        }

        private void AddRepository(IServiceCollection services)
        {
            services.AddScoped<ICategoriesRepo, CategoriesRepo>();
            services.AddScoped<ICountriesRepo, CountriesRepo>();
            services.AddScoped<IFacultiesRepo, FacultiesRepo>();
            services.AddScoped<IPurchasedOptionTypeRepo, PurchasedOptionTypeRepo>();
            services.AddScoped<IPurchaseOptionsRepo, PurchaseOptionsRepo>();
            services.AddScoped<IWebinarRepo, WebinarRepo>();
            services.AddScoped<IInvoiceRepo, InvoiceRepo>();
            services.AddScoped<IWebinarPurchasedOptionsDetailsRepo, WebinarPurchasedOptionsDetailsRepo>();
            services.AddScoped<IOrdersRepo, OrdersRepo>();
            services.AddScoped<IUserCartRepo, UserCartRepo>();
            services.AddScoped<ILoginRepo, LoginRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IWebinarTypeRepo, WebinarTypeRepo>();
            services.AddScoped<ICompaniesRepo, CompaniesRepo>();
        }
    }
}
