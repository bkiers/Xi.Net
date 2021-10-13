namespace Xi.BlazorApp.Stores.Features.ExtendClock.Effects
{
  using System.Threading.Tasks;
  using Fluxor;
  using Xi.BlazorApp.Services;
  using Xi.BlazorApp.Stores.Features.ExtendClock.Actions;

  public class GetExtendClockEffect : Effect<GetExtendClockAction>
  {
    private readonly IGameService gameService;

    public GetExtendClockEffect(IGameService gameService)
    {
      this.gameService = gameService;
    }

    public override Task HandleAsync(GetExtendClockAction action, IDispatcher dispatcher)
    {
      var isPossible = this.gameService.CanExtendClock(action.PlayerId, action.GameId);

      dispatcher.Dispatch(new GetExtendClockResult(isPossible));

      return Task.CompletedTask;
    }
  }
}