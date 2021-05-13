namespace Xi.BlazorApp.Stores.States
{
  using Xi.Models.Game;

  public class GameState : RootState
  {
    public GameState(bool isLoading, string? currentErrorMessage, Game? game)
      : base(isLoading, currentErrorMessage)
    {
      this.Game = game;
    }

    public Game? Game { get; }
  }
}