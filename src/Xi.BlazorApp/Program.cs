namespace Xi.BlazorApp
{
  using System;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.Hosting;

  public class Program
  {
    public static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
      return Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
          var settingsFile = $"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json";

          Console.WriteLine($">>> Loading: {settingsFile}");
          config.AddJsonFile(settingsFile);
        })
        .ConfigureWebHostDefaults(webBuilder =>
        {
          webBuilder.UseStartup<Startup>();
        });
    }
  }
}
