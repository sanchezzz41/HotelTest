using System;
using HotelTest.Database;
using HotelTest.Domain;
using HotelTest.Domain.Entities;
using HotelTest.Identity;
using HotelTest.Sms;
using HotelTest.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace HotelTest.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Сервисы(нужно что бы бд иницилизировать)
        /// </summary>
        private IServiceProvider ServiceProvider { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(opt =>
            {
                opt.UseNpgsql(Configuration["ConnectionStrings:ConnectionToDb"],
                    assemb => assemb.MigrationsAssembly("HotelTest.Web"));
            });

            //Сервисы для аутификации пользователя и валидации пароля
            services.AddScoped<IHashProvider, Md5HashService>();
            services.AddScoped<IPasswordHasher<User>, Md5PasswordHasher>();

            services.AddIdentity<User, Role>()
                .AddRoleStore<RoleStore>()
                .AddUserStore<IdentityStore>()
                .AddPasswordValidator<Md5PasswordValidator>()
                .AddDefaultTokenProviders();

            //services.AddMemoryCache();


            services.AddMvc(x =>
            {
                x.Filters.Add<ErrorFilter>();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {Title = "My API", Version = "v1"});
            });

            //Добавление сервисов из Domain
            services.AddDomainServices(
                x =>
                {
                    x.RoomCount = 100;
                    x.PriceForStandard = 500;
                    x.PriceForHalfLux = 1000;
                    x.PriceForLux = 5000;
                }
            );
            //Добавления смс сервиса
            services.AddSmsService();

            ServiceProvider = services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddConsole(Configuration.GetSection("Logging"));
                loggerFactory.AddDebug();
            }

            app.UseAuthentication();
            app.UseSwagger();

            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "My api");
            });
            app.UseMvc();

            ServiceProvider.GetService<DatabaseContext>().Database.Migrate();
            ServiceProvider.GetService<DatabaseContext>().Initializer(ServiceProvider).Wait();
        
        }
    }
}
