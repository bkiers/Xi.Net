namespace Xi.BlazorApp.Stores.Features.Game.Actions.Moves
{
  using Xi.BlazorApp.Models;
  using Xi.BlazorApp.Stores.Features.Shared.Actions;

  public class ConfirmMoveFailureAction : FailureAction
  {
    public ConfirmMoveFailureAction(GameModel gameModel, string errorMessage)
      : base(errorMessage)
    {
      this.GameModel = gameModel;
    }

    public GameModel GameModel { get; }
  }
}
