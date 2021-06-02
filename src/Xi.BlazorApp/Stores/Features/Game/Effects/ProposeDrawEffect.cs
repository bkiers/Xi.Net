namespace Xi.BlazorApp.Stores.Features.Game.Effects
{
  using System;
  using System.Threading.Tasks;
  using Fluxor;
  using Xi.BlazorApp.Services;
  using Xi.BlazorApp.Stores.Features.Game.Actions.EndGame;

  public class ProposeDrawEffect : Effect<ProposeDrawAction>
  {
    private readonly IGameService gameService;

    public ProposeDrawEffect(IGameService gameService)
    {
      this.gameService = gameService;
    }

    public override Task HandleAsync(ProposeDrawAction action, IDispatcher dispatcher)
    {
      try
      {
        var game = this.gameService.ProposeDraw(action.Player.Id, action.GameModel.Game.Id);

        dispatcher.Dispatch(new ProposeDrawSuccessAction(game));
      }
      catch (Exception e)
      {
        dispatcher.Dispatch(new ProposeDrawFailureAction(action.GameModel, e.Message));
      }

      return Task.CompletedTask;
    }
  }
}