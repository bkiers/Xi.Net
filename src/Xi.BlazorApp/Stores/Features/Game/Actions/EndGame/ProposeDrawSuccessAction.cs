namespace Xi.BlazorApp.Stores.Features.Game.Actions.EndGame;

using Xi.BlazorApp.Models;

public class ProposeDrawSuccessAction
{
  public ProposeDrawSuccessAction(GameModel gameModel)
  {
    this.GameModel = gameModel;
  }

  public GameModel GameModel { get; }
}