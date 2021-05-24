namespace Xi.BlazorApp
{
  using System;
  using System.Threading.Tasks;
  using Fluxor;
  using Microsoft.AspNetCore.Authentication.Cookies;
  using Microsoft.AspNetCore.Authentication.OpenIdConnect;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.AspNetCore.Http;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Hosting;
  using Toolbelt.Blazor.Extensions.DependencyInjection;
  using Xi.BlazorApp.Config;
  using Xi.BlazorApp.Hubs;
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

      services.Configure<CookiePolicyOptions>(options =>
      {
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
      });

      services.AddFluxor(o => o
        .ScanAssemblies(typeof(Program).Assembly)
        .UseReduxDevTools());

      services.AddDbContext<XiContext>(options =>
        options.UseNpgsql(this.Configuration.GetConnectionString("DefaultConnection")));

      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
      })
      .AddCookie()
      .AddOpenIdConnect("Auth0", options =>
      {
        // Set the authority to your Auth0 domain
        options.Authority = $"https://{this.Configuration["Auth0:Domain"]}";

        // Configure the Auth0 Client ID and Client Secret
        options.ClientId = this.Configuration["Auth0:ClientId"];
        options.ClientSecret = this.Configuration["Auth0:ClientSecret"];

        // Set response type to code
        options.ResponseType = "code";

        // Configure the scope
        options.Scope.Clear();
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("email");

        // Set the callback path, so Auth0 will call back to http://localhost:3000/callback
        // Also ensure that you have added the URL as an Allowed Callback URL in your Auth0 dashboard
        options.CallbackPath = new PathString("/callback");

        // Configure the Claims Issuer to be Auth0
        options.ClaimsIssuer = "Auth0";

        options.Events = new OpenIdConnectEvents
        {
          // handle the logout redirection
          OnRedirectToIdentityProviderForSignOut = context =>
          {
            var logoutUri = $"https://{this.Configuration["Auth0:Domain"]}/v2/logout?client_id={this.Configuration["Auth0:ClientId"]}";
            var postLogoutUri = context.Properties.RedirectUri;

            if (!string.IsNullOrEmpty(postLogoutUri))
            {
              if (postLogoutUri.StartsWith("/"))
              {
                // transform to absolute
                var request = context.Request;
                postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
              }

              logoutUri += $"&returnTo={Uri.EscapeDataString(postLogoutUri)}";
            }

            context.Response.Redirect(logoutUri);
            context.HandleResponse();

            return Task.CompletedTask;
          },
        };
      });

      services.AddHttpContextAccessor();

      services.AddScoped<IGameService, GameService>();
      services.AddScoped<IPlayerService, PlayerService>();
      services.AddScoped<Current>();

      services.AddHotKeys();
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
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapBlazorHub();
        endpoints.MapFallbackToPage("/_Host");
        endpoints.MapHub<GamesHub>(GamesHub.HubUrl);
      });
    }
  }
}
