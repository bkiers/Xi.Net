namespace Xi.BlazorApp.Pages
{
  using System.Linq;
  using System.Security.Claims;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Authentication;
  using Microsoft.AspNetCore.Authentication.Cookies;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.AspNetCore.Mvc.RazorPages;
  using Microsoft.Extensions.Logging;
  using Xi.BlazorApp.Services;

  [AllowAnonymous]
  public class Login : PageModel
  {
    private readonly IPlayerService playerService;
    private readonly ILogger<Login> logger;

    public Login(IPlayerService playerService, ILogger<Login> logger)
    {
      this.playerService = playerService;
      this.logger = logger;
    }

    public IActionResult OnGetAsync(string redirectUrl = "/")
    {
      // Request a redirect to the external login provider.
      var authenticationProperties = new AuthenticationProperties
      {
        RedirectUri = this.Url.Page(
            "./Login",
            pageHandler: "Callback",
            values: new { redirectUrl }),
      };

      return new ChallengeResult("Google", authenticationProperties);
    }

    public async Task<IActionResult> OnGetCallbackAsync(string? redirectUrl, string? remoteError)
    {
      this.logger.LogInformation($"redirectUrl={redirectUrl}, remoteError={remoteError}");

      // Get the information about the user from the external login provider
      var googleUser = this.User.Identities.FirstOrDefault();
      var email = googleUser?.FindFirst(ClaimTypes.Email)?.Value;

      this.logger.LogInformation($"googleUser={googleUser}, email={email}, IsAuthenticated={googleUser?.IsAuthenticated}");

      if (googleUser?.IsAuthenticated == true)
      {
        var exists = this.playerService.FindByEmail(email ?? string.Empty);

        if (exists == null)
        {
          await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

          return this.LocalRedirect("/nonono");
        }

        var authProperties = new AuthenticationProperties
        {
          IsPersistent = true,
          RedirectUri = this.Request.Host.Value,
        };

        await this.HttpContext.SignInAsync(
          CookieAuthenticationDefaults.AuthenticationScheme,
          new ClaimsPrincipal(googleUser),
          authProperties);
      }

      return this.LocalRedirect(redirectUrl ?? "/");
    }
  }
}
