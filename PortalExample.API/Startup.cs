using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PortalExample.API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PortalExample.API.Models;
using AutoMapper;

namespace PortalExample.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // rejestracja usługi dla aplikacji
        // wbudowany system zalżności
        //usłigi sa szerokim  pojęciem 
        //modularne i terzeba wszystko dodawać
        //własne czy framework
        public void ConfigureServices(IServiceCollection services)// automatycznie wykonywana przez system
        {
            // services.AddMvc(option => option.EnableEndpointRouting = false);
            // services.AddControllers(); 
            services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc(option => option.EnableEndpointRouting = false);
            // .AddJsonOptions(option =>{option.JsonSerializerOptions});
            services.AddAutoMapper();
            services.AddCors();
            services.AddTransient<Seed>();
            services.AddScoped<IGenericRepository, GenericRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();//jedna instancja dla tego samego żądania tego samego www
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //potok żadania np porgramowanie posrednie np na komponnety przychodzące przez http
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Seed seed)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            seed.SeedUseds();
            app.UseCors(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            // app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();//potrzebuje routing oparty na atrybtach
            // app.UseRouting();

            // app.UseAuthorization();

            // app.UseEndpoints(endpoints =>
            // {
            //     endpoints.MapControllers();
            // });
        }
    }
}
//polecenie cmd: dotnet watch run => widzi zmiany i je przeładuje na urucmionym serwerze
