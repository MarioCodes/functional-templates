using csharp_net_core_3._1_DI.consumer;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace csharp_net_core_3._1_DI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
