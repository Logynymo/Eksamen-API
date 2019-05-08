using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Eksamen_API
{
    /// <summary>
    /// Default generated Program class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// CreateWebHostBuilder with string array called args as parameter, it then builds and runs.
        /// </summary>
        /// <param name="args">String array called args</param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Method used only by the Constructor in this class.
        /// It creates a default WebHostBuilder with the string array called args.
        /// .UseStartup is using Startup.cs for... starting up.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
