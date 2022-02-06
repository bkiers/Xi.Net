namespace Xi.BlazorApp.Services;

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

  public int? PossibleLoggedInPlayerId()
  {
    return this.LoggedIn() ? this.LoggedInPlayerId() : null;
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

    return this.playerService.FindByEmail(email)!;
  }

  public bool LoggedIn()
  {
    var user = this.httpContextAccessor.HttpContext?.User;
    var email = user?.FindFirst(ClaimTypes.Email)?.Value;

    return this.playerService.FindByEmail(email ?? string.Empty) != null;
  }

  public Settings Settings()
  {
    return this.LoggedInPlayer().Settings;
  }
}