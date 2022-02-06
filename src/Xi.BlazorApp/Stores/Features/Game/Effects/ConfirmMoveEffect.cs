namespace Xi.BlazorApp.Stores.Features.Game.Effects;

using System;
using System.Threading.Tasks;
using Fluxor;
using Xi.BlazorApp.Services;
using Xi.BlazorApp.Stores.Features.Game.Actions.Moves;

public class ConfirmMoveEffect : Effect<ConfirmMoveAction>
{
  private readonly IGameService gameService;

  public ConfirmMoveEffect(IGameService gameService)
  {
    this.gameService = gameService;
  }

  public override Task HandleAsync(ConfirmMoveAction action, IDispatcher dispatcher)
  {
    try
    {
      var move = action.GameModel.Game.Moves[action.Index];
      var game = this.gameService.Move(action.LoggedInUserId, action.GameModel.Game.Id, move.FromCell, move.ToCell);

      dispatcher.Dispatch(new ConfirmMoveSuccessAction(game!));
    }
    catch (Exception e)
    {
      dispatcher.Dispatch(new ConfirmMoveFailureAction(action.GameModel, e.Message));
    }

    return Task.CompletedTask;
  }
}