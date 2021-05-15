namespace Xi.BlazorApp.Stores.Features.Games.Actions.LoadGames
{
  using System.Collections.Generic;
  using Xi.BlazorApp.Models;

  public class LoadGamesSuccessAction
  {
    public LoadGamesSuccessAction(List<GameViewModel> gameViewModels)
    {
      this.GameViewModels = gameViewModels;
    }

    public List<GameViewModel> GameViewModels { get; }
  }
}