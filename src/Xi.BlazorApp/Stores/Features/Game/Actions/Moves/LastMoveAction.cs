namespace Xi.BlazorApp.Stores.Features.Game.Actions.Moves;

using Xi.BlazorApp.Models;

public class LastMoveAction
{
  public LastMoveAction(GameModel gameModel)
  {
    this.GameModel = gameModel;
  }

  public GameModel GameModel { get; }
}