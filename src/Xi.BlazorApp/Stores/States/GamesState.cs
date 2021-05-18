namespace Xi.BlazorApp.Stores.States
{
  using System.Collections.Generic;
  using Xi.BlazorApp.Models;

  public class GamesState : RootState
  {
    public GamesState(bool isLoading, string? errorMessage, List<GameModel>? gameViewModels)
      : base(isLoading, errorMessage)
    {
      this.GameViewModels = gameViewModels;
    }

    public List<GameModel>? GameViewModels { get; }
  }
}