namespace Xi.BlazorApp.Stores.Features.Game.Actions.LoadGame
{
  using Xi.BlazorApp.Models;

  public class LoadGameSuccessAction
  {
    public LoadGameSuccessAction(GameModel gameModel)
    {
      this.GameModel = gameModel;
    }

    public GameModel GameModel { get; }
  }
}