using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using TechReads.Web;

Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
    .Build()
    .Run();

