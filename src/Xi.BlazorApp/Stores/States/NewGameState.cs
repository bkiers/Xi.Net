namespace Xi.BlazorApp.Stores.States
{
  using Xi.BlazorApp.Models;

  public class NewGameState : RootState
  {
    public NewGameState(bool isLoading, string? errorMessage, GameViewModel? gameViewModel)
      : base(isLoading, errorMessage)
    {
      this.GameViewModel = gameViewModel;
    }

    public GameViewModel? GameViewModel { get; }
  }
}