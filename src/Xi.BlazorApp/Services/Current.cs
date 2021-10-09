namespace Xi.BlazorApp.Services
{
  using System.Linq;
  using System.Security.Claims;
  using Microsoft.AspNetCore.Http;
  using Xi.Models;
  using Xi.Models.Game;

  public class Current
  {
    private readonly IPlayerService playerService;
    private readonly IHttpContextAccessor httpContextAccessor;

    public Current(IPlayerService playerService, IHttpContextAccessor httpContextAccessor)
    {
      this.playerService = playerService;
      this.httpContextAccessor = httpContextAccessor;
    }

    public int LoggedInPlayerId()
    {
      return this.LoggedInPlayer().Id;
    }

    public Player LoggedInPlayer()
    {
      var user = this.httpContextAccessor.HttpContext?.User!;

      var name = user.FindFirst(ClaimTypes.GivenName)?.Value;
      var email = user.FindFirst(ClaimTypes.Email)!.Value;

      return this.playerService.FindByEmailOrCreate(email, name ?? email.Split('@').First());
    }

    public bool LoggedIn()
    {
      var user = this.httpContextAccessor.HttpContext?.User;

      return !string.IsNullOrEmpty(user?.FindFirst(ClaimTypes.Email)?.Value);
    }

    public Settings Settings()
    {
      return this.LoggedInPlayer().Settings;
    }
  }
}