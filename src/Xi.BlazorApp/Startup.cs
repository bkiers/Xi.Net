namespace Xi.BlazorApp
{
  using Fluxor;
  using Microsoft.AspNetCore.Authentication;
  using Microsoft.AspNetCore.Authentication.Cookies;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.AspNetCore.Http;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Hosting;
  using Toolbelt.Blazor.Extensions.DependencyInjection;
  using Xi.BlazorApp.BackgroundServices;
  using Xi.BlazorApp.Config;
  using Xi.BlazorApp.EventHandlers;
  using Xi.BlazorApp.Services;
  using Xi.Database;

  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      this.Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.Configure<XiConfig>(this.Configuration.GetSection(nameof(XiConfig)));

      services.AddRazorPages();
      services.AddServerSideBlazor();

      services.AddDbContext<XiContext>(options =>
        options.UseNpgsql(this.Configuration.GetConnectionString("DefaultConnection")));

      services.AddFluxor(o => o
        .ScanAssemblies(typeof(Program).Assembly)
        .UseReduxDevTools());

      services.AddScoped<IGameService, GameService>();
      services.AddScoped<IPlayerService, PlayerService>();
      services.AddScoped<IEmailService, EmailService>();
      services.AddScoped<Current>();

      services.AddHotKeys();

      services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie();

      services.AddAuthentication().AddGoogle(options =>
      {
        options.ClientId = this.Configuration["Google:ClientId"];
        options.ClientSecret = this.Configuration["Google:ClientSecret"];
        options.ClaimActions.MapJsonKey("urn:google:profile", "link");
      });

      services.AddHttpContextAccessor();
      services.AddScoped<HttpContextAccessor>();

      services.AddSingleton(this.Configuration);
      services.AddSingleton(services);

      services.AddHostedService<TimeCheckService>();

      services.AddEventBus(builder =>
      {
        builder.AddInMemoryEventBus(subscriber =>
        {
          subscriber.Subscribe<EmailReminderEventHandler.Event, EmailReminderEventHandler>();
          subscriber.Subscribe<TimeRanOutEventHandler.Event, TimeRanOutEventHandler>();
          subscriber.Subscribe<NewGameEventHandler.Event, NewGameEventHandler>();
          subscriber.Subscribe<AcceptNewGameEventHandler.Event, AcceptNewGameEventHandler>();
          subscriber.Subscribe<DeclineNewGameEventHandler.Event, DeclineNewGameEventHandler>();
          subscriber.Subscribe<GameOverEventHandler.Event, GameOverEventHandler>();
          subscriber.Subscribe<MoveMadeEventHandler.Event, MoveMadeEventHandler>();
          subscriber.Subscribe<DrawProposalEventHandler.Event, DrawProposalEventHandler>();
          subscriber.Subscribe<DeclineDrawProposalEventHandler.Event, DeclineDrawProposalEventHandler>();
        });
      });
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

      app.UseCookiePolicy();
      app.UseAuthentication();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapBlazorHub();
        endpoints.MapFallbackToPage("/_Host");
      });
    }
  }
}
