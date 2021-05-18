namespace Xi.BlazorApp.Stores.Features.Game.Actions.CycleMoves
{
  using Xi.BlazorApp.Models;

  public class PreviousMoveAction
  {
    public PreviousMoveAction(GameModel gameModel)
    {
      this.GameModel = gameModel;
    }

    public GameModel GameModel { get; }
  }
}