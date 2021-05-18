namespace Xi.BlazorApp.Stores.Features.Games.Actions.LoadGames
{
  using System.Collections.Generic;
  using Xi.BlazorApp.Models;

  public class LoadGamesSuccessAction
  {
    public LoadGamesSuccessAction(List<GameModel> gameViewModels)
    {
      this.GameViewModels = gameViewModels;
    }

    public List<GameModel> GameViewModels { get; }
  }
}