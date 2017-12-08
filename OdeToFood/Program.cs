using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace OdeToFood
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        /// <summary>
        /// 1. Use Kestrel web Server
        /// 2. IIS Integration
        /// 3. Looging
        /// 4.IConfiguration service made available
        ///     -Json file(appsettings.json)
        ///     -user Secrets
        ///     -Environment variables
        ///     -command line arguments
        /// </summary>
        /// <param name="args"> Args coming from main method</param>
        /// <returns></returns>
        public static IWebHost BuildWebHost(string[] args) => WebHost.CreateDefaultBuilder(args)
                                                                    .UseStartup<Startup>()
                                                                    .Build();
    }
}
