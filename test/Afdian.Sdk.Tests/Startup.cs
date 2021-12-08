using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Afdian.Sdk.Tests
{
    public class Startup
    {
        public void ConfigureHost(IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureHostConfiguration(configurationBuilder =>
            {
                configurationBuilder.AddUserSecrets("0377da50-fdb8-4811-bab1-7299c2657571", true);
            });
        }


        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddTransient<IDependency, DependencyClass>();
        }

    }
}
