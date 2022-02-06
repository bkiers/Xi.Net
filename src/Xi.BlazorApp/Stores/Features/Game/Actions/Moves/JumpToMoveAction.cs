namespace Xi.BlazorApp.Stores.Features.Game.Actions.Moves;

using Xi.BlazorApp.Models;

public class JumpToMoveAction
{
  public JumpToMoveAction(GameModel gameModel, int index)
  {
    this.GameModel = gameModel;
    this.Index = index;
  }

  public GameModel GameModel { get; }

  public int Index { get; }
}