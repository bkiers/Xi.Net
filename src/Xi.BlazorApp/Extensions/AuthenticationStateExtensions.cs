namespace Xi.BlazorApp.Extensions
{
  using System.Linq;
  using Microsoft.AspNetCore.Components.Authorization;

  public static class AuthenticationStateExtensions
  {
    public static string Get(this AuthenticationState state, string type, bool exactMatch = true)
    {
      return state.User.Claims
        .Where(c => exactMatch ? c.Type.Equals(type) : c.Type.Contains(type))
        .Select(c => c.Value)
        .FirstOrDefault() ?? string.Empty;
    }
  }
}