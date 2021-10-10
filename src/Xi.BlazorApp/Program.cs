namespace Xi.BlazorApp
{
  using System;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Hosting;

  public class Program
  {
    public static void Main(string[] args)
    {
      var host = CreateHostBuilder(args).Build();

      host.Services.GetRequiredService<IHostApplicationLifetime>()
        .ApplicationStopped.Register(() =>
        {
          Console.WriteLine("Xi.Net is shut down");
        });

      host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
      return Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
          webBuilder.UseStartup<Startup>();
        });
    }
  }
}
