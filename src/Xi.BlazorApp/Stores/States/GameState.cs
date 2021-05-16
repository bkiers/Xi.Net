namespace Xi.BlazorApp.Stores.States
{
  using Xi.BlazorApp.Models;

  public class GameState : RootState
  {
    public GameState(bool isLoading, string? errorMessage, GameViewModel? gameViewModel)
      : base(isLoading, errorMessage)
    {
      this.GameViewModel = gameViewModel;
    }

    public GameViewModel? GameViewModel { get; }
  }
}