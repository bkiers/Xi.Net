namespace Xi.BlazorApp.Stores.Features.Game.Actions.DrawGame
{
  using Xi.BlazorApp.Models;
  using Xi.Models.Game;

  public class HandleDrawProposalSuccessAction
  {
    public HandleDrawProposalSuccessAction( GameModel gameModel)
    {
      this.GameModel = gameModel;
    }

    public GameModel GameModel { get; }
  }
}