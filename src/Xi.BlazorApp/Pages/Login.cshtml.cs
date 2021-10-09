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

  [AllowAnonymous]
  public class Login : PageModel
  {
    private readonly ILogger<Login> logger;

    public Login(ILogger<Login> logger)
    {
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
      this.logger.LogInformation($"googleUser={googleUser}, IsAuthenticated={googleUser?.IsAuthenticated}");

      if (googleUser?.IsAuthenticated == true)
      {
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
