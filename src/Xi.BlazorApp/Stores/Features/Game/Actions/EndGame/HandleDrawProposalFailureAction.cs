namespace Xi.BlazorApp.Stores.Features.Game.Actions.EndGame;

using Xi.BlazorApp.Models;
using Xi.BlazorApp.Stores.Features.Shared.Actions;

public class HandleDrawProposalFailureAction : FailureAction
{
  public HandleDrawProposalFailureAction(GameModel gameModel, string errorMessage)
    : base(errorMessage)
  {
    this.GameModel = gameModel;
  }

  public GameModel GameModel { get; }
}