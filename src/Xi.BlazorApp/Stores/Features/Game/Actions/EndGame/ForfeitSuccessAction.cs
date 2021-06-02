namespace Xi.BlazorApp.Stores.Features.Game.Actions.EndGame
{
  using Xi.BlazorApp.Models;
  using Xi.Models.Game;

  public class ForfeitSuccessAction
  {
    public ForfeitSuccessAction(GameModel gameModel)
    {
      this.GameModel = gameModel;
    }

    public GameModel GameModel { get; }
  }
}