namespace Xi.BlazorApp.Pages
{
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Authentication;
  using Microsoft.AspNetCore.Mvc.RazorPages;

  public class Login : PageModel
  {
    public async Task OnGet(string redirectUri)
    {
      await this.HttpContext.ChallengeAsync("Auth0", new AuthenticationProperties
      {
        RedirectUri = redirectUri,
      });
    }
  }
}
