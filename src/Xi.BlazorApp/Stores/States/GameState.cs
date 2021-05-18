namespace Xi.BlazorApp.Stores.States
{
  using Xi.BlazorApp.Models;

  public class GameState : RootState
  {
    public GameState(bool isLoading, string? errorMessage, GameModel? gameModel)
      : base(isLoading, errorMessage)
    {
      this.GameModel = gameModel;
    }

    public GameModel? GameModel { get; }
  }
}