namespace Xi.BlazorApp.Stores.States;

using Xi.BlazorApp.Models;

public class NewGameState : RootState
{
  public NewGameState(bool isLoading, string? errorMessage, GameModel? gameViewModel)
    : base(isLoading, errorMessage)
  {
    this.GameViewModel = gameViewModel;
  }

  public GameModel? GameViewModel { get; }
}