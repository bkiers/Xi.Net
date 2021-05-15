namespace Xi.BlazorApp.Stores.Features.Game.Effects
{
  using System;
  using System.Threading.Tasks;
  using Fluxor;
  using Xi.BlazorApp.Services;
  using Xi.BlazorApp.Stores.Features.Game.Actions.ClickBoard;
  using Xi.BlazorApp.Stores.Features.Game.Actions.LoadGame;

  public class ClickBoardEffect : Effect<ClickBoardAction>
  {
    private readonly IGameService gameService;

    public ClickBoardEffect(IGameService gameService)
    {
      this.gameService = gameService;
    }

    public override Task HandleAsync(ClickBoardAction action, IDispatcher dispatcher)
    {
      var gameViewModel = action.GameViewModel;

      try
      {
        gameViewModel.Click(action.ClickedCell);

        // TODO: other action with new viewmodel
        dispatcher.Dispatch(new LoadGameSuccessAction(gameViewModel!));
      }
      catch (Exception e)
      {
        // TODO: other action with old viewmodel
        dispatcher.Dispatch(new LoadGameFailureAction(e.Message));
      }

      return Task.CompletedTask;
    }
  }
}