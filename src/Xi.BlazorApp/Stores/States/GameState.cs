namespace Xi.BlazorApp.Stores.States
{
  using Xi.BlazorApp.Models;

  public class GameState : RootState
  {
    public GameState(bool isLoading, string? currentErrorMessage, GameViewModel? gameViewModel)
      : base(isLoading, currentErrorMessage)
    {
      this.GameViewModel = gameViewModel;
    }

    public GameViewModel? GameViewModel { get; }
  }
}