namespace Xi.BlazorApp.Stores.States
{
  using System.Collections.Generic;
  using Xi.Models.Game;

  public record PlayersState(
    bool? IsLoading = null,
    IList<Player>? Players = null);
}