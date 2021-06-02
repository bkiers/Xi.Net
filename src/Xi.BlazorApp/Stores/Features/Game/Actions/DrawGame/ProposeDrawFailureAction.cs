namespace Xi.BlazorApp.Stores.Features.Game.Actions.DrawGame
{
  using Xi.BlazorApp.Models;
  using Xi.BlazorApp.Stores.Features.Shared.Actions;

  public class ProposeDrawFailureAction : FailureAction
  {
    public ProposeDrawFailureAction(GameModel gameModel, string errorMessage)
      : base(errorMessage)
    {
      this.GameModel = gameModel;
    }

    public GameModel GameModel { get; }
  }
}