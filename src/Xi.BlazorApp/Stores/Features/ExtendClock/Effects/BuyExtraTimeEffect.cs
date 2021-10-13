namespace Xi.BlazorApp.Stores.Features.ExtendClock.Effects
{
  using System.Threading.Tasks;
  using Fluxor;
  using Xi.BlazorApp.Services;
  using Xi.BlazorApp.Stores.Features.ExtendClock.Actions;
  using Xi.BlazorApp.Stores.Features.Game.Actions.LoadGame;

  public class BuyExtraTimeEffect : Effect<BuyExtraTimeAction>
  {
    private readonly IGameService gameService;

    public BuyExtraTimeEffect(IGameService gameService)
    {
      this.gameService = gameService;
    }

    public override Task HandleAsync(BuyExtraTimeAction action, IDispatcher dispatcher)
    {
      var gameViewModel = this.gameService.BuyExtraTime(action.PlayerId, action.GameId);

      dispatcher.Dispatch(new LoadGameSuccessAction(gameViewModel));

      return Task.CompletedTask;
    }
  }
}