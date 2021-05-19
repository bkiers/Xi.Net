namespace Xi.BlazorApp.Stores.Features.Game.Effects
{
  using System;
  using System.Threading.Tasks;
  using Fluxor;
  using Xi.BlazorApp.Services;
  using Xi.BlazorApp.Stores.Features.Game.Actions.LoadGame;
  using Xi.BlazorApp.Stores.Features.Game.Actions.StartGame;

  public class AcceptGameEffect : Effect<AcceptGameAction>
  {
    private readonly IGameService gameService;

    public AcceptGameEffect(IGameService gameService)
    {
      this.gameService = gameService;
    }

    public override Task HandleAsync(AcceptGameAction action, IDispatcher dispatcher)
    {
      try
      {
        var game = this.gameService.Accept(action.Player.Id, action.GameModel.Game.Id);

        dispatcher.Dispatch(new LoadGameSuccessAction(game));
      }
      catch (Exception e)
      {
        dispatcher.Dispatch(new LoadGameFailureAction(e.Message));
      }

      return Task.CompletedTask;
    }
  }
}