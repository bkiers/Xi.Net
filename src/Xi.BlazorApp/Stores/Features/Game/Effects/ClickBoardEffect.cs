namespace Xi.BlazorApp.Stores.Features.Game.Effects;

using System;
using System.Threading.Tasks;
using Fluxor;
using Xi.BlazorApp.Stores.Features.Game.Actions.ClickBoard;

public class ClickBoardEffect : Effect<ClickBoardAction>
{
  public override Task HandleAsync(ClickBoardAction action, IDispatcher dispatcher)
  {
    var gameViewModel = action.GameModel;

    try
    {
      gameViewModel.Click(action.ClickedCell);

      dispatcher.Dispatch(new ClickBoardValidAction(action.ClickedCell, gameViewModel!));
    }
    catch (Exception e)
    {
      gameViewModel.UndoFirstClick();

      dispatcher.Dispatch(new ClickBoardInvalidAction(action.ClickedCell, gameViewModel!, e.Message));
    }

    return Task.CompletedTask;
  }
}