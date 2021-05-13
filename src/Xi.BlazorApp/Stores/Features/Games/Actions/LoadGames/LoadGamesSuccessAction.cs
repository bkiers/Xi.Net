namespace Xi.BlazorApp.Stores.Features.Games.Actions.LoadGames
{
  using System.Collections.Generic;
  using Xi.Models.Game;

  public class LoadGamesSuccessAction
  {
    public LoadGamesSuccessAction(List<Game> games)
    {
      this.Games = games;
    }

    public List<Game> Games { get; }
  }
}