using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelTest.Database;
using HotelTest.Domain.Entities;
using HotelTest.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HotelTest.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private IServiceProvider ServiceProvider { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(opt =>
            {
                opt.UseNpgsql(Configuration["ConnectionStrings:ConnectionToDb"],
                    assemb => assemb.MigrationsAssembly("HotelTest.Web"));
            });

            //Сервисы для аутификации и валидации пароля
            services.AddScoped<IHashProvider, Md5HashService>();
            services.AddScoped<IPasswordHasher<User>, Md5PasswordHasher>();
            services.AddIdentity<User, Role>()
                .AddRoleStore<RoleStore>()
                .AddUserStore<IdentityStore>()
                .AddPasswordValidator<Md5PasswordValidator>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            ServiceProvider = services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            ServiceProvider.GetService<DatabaseContext>().Initializer(ServiceProvider).Wait();
            app.UseMvc();
        }
    }
}
