namespace Xi.BlazorApp.Stores.Features.Game.Actions.LoadGame
{
  using Xi.Models.Game;

  public class LoadGameSuccessAction
  {
    public LoadGameSuccessAction(Game game)
    {
      this.Game = game;
    }

    public Game Game { get; }
  }
}