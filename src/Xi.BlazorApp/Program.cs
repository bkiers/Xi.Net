namespace Xi.BlazorApp;

using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
  public const string Version = "0.1.0";
  public static readonly string Build = ReadFile("./build.txt");

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

  private static string ReadFile(string fileName)
  {
    try
    {
      return System.IO.File.ReadAllText(fileName).Trim();
    }
    catch
    {
      return "local development";
    }
  }
}