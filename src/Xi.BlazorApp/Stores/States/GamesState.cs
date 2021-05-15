namespace Xi.BlazorApp.Stores.States
{
  using System.Collections.Generic;
  using Xi.BlazorApp.Models;

  public class GamesState : RootState
  {
    public GamesState(bool isLoading, string? currentErrorMessage, List<GameViewModel>? gameViewModels)
      : base(isLoading, currentErrorMessage)
    {
      this.GameViewModels = gameViewModels;
    }

    public List<GameViewModel>? GameViewModels { get; }
  }
}