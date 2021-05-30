namespace Xi.BlazorApp.Pages
{
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Authentication;
  using Microsoft.AspNetCore.Authentication.Cookies;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.AspNetCore.Mvc.RazorPages;

  public class Logout : PageModel
  {
    public async Task<IActionResult> OnGetAsync(string returnUrl = "/")
    {
      // Clear the existing external cookie.
      await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

      return this.LocalRedirect(returnUrl);
    }
  }
}