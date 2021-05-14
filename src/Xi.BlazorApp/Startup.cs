namespace Xi.BlazorApp
{
  using Fluxor;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Hosting;
  using Xi.Database;
  using Xi.Services;

  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      this.Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddRazorPages();
      services.AddServerSideBlazor();

      services.AddFluxor(o => o
        .ScanAssemblies(typeof(Program).Assembly)
        .UseReduxDevTools());

      services.AddDbContext<XiContext>(options =>
        options.UseNpgsql(this.Configuration.GetConnectionString("DefaultConnection")));

      services.AddScoped<IGameService, GameService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope())
      {
        var context = serviceScope!.ServiceProvider.GetRequiredService<XiContext>();
        context!.Database.Migrate();
      }

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapBlazorHub();
        endpoints.MapFallbackToPage("/_Host");
      });
    }
  }
}
