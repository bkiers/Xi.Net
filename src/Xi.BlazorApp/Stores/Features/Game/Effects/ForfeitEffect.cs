namespace Xi.BlazorApp.Stores.Features.Game.Effects
{
  using System;
  using System.Threading.Tasks;
  using Fluxor;
  using Xi.BlazorApp.Services;
  using Xi.BlazorApp.Stores.Features.Game.Actions.EndGame;

  public class ForfeitEffect : Effect<ForfeitAction>
  {
    private readonly IGameService gameService;

    public ForfeitEffect(IGameService gameService)
    {
      this.gameService = gameService;
    }

    public override Task HandleAsync(ForfeitAction action, IDispatcher dispatcher)
    {
      var game = this.gameService.Forfeit(action.Player.Id, action.GameModel.Game.Id);

      dispatcher.Dispatch(new ForfeitSuccessAction(game));

      return Task.CompletedTask;
    }
  }
}