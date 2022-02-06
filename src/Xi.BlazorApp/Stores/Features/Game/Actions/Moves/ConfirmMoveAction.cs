namespace Xi.BlazorApp.Stores.Features.Game.Actions.Moves;

using Xi.BlazorApp.Models;

public class ConfirmMoveAction
{
  public ConfirmMoveAction(int loggedInUserId, GameModel gameModel, int index)
  {
    this.LoggedInUserId = loggedInUserId;
    this.GameModel = gameModel;
    this.Index = index;
  }

  public int LoggedInUserId { get; }

  public GameModel GameModel { get; }

  public int Index { get; }
}