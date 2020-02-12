using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PortalExample.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();// budowany jest host który konfiguruje serwer
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>// konfigurrowanie serwera wwww o nazwie kestrel, wbudowany serwer siecowego który zostanie zbudowany. obsuguje nasza aplikację
                {
                    webBuilder.UseStartup<Startup>();// Tu jest ustwaniana klasa startowa
                });
    }
}
//Możliwośc skonfiguruje naszą konfiguracje do IIS