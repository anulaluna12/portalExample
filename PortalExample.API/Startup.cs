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

using Microsoft.EntityFrameworkCore;

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
            services.AddDbContext<DataContext>(x =>x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
             services.AddMvc(option => option.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //potok żadania np porgramowanie posrednie np na komponnety przychodzące przez http
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();
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
