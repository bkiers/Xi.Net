namespace Xi.BlazorApp.Stores.Features.Game.Actions.Moves;

using Xi.BlazorApp.Models;

public class NextMoveAction
{
  public NextMoveAction(GameModel gameModel)
  {
    this.GameModel = gameModel;
  }

  public GameModel GameModel { get; }
}