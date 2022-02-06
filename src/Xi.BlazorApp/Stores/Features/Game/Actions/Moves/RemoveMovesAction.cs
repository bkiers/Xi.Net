namespace Xi.BlazorApp.Stores.Features.Game.Actions.Moves;

using Xi.BlazorApp.Models;

public class RemoveMovesAction
{
  public RemoveMovesAction(GameModel gameModel, int fromIndex)
  {
    this.GameModel = gameModel;
    this.FromIndex = fromIndex;
  }

  public GameModel GameModel { get; }

  public int FromIndex { get; }
}