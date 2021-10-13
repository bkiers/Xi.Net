namespace Xi.BlazorApp.Stores.Features.Players.Actions
{
  using System.Collections.Generic;
  using Xi.Models.Game;

  public record GetPlayersResult(IList<Player> Players);
}