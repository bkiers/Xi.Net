namespace Xi.BlazorApp.Stores.Features.Game.Effects
{
  using System.Threading.Tasks;
  using Fluxor;
  using Xi.BlazorApp.Services;
  using Xi.BlazorApp.Stores.Features.Game.Actions.LoadGame;
  using Xi.BlazorApp.Stores.Features.Game.Actions.StartGame;

  public class DeclineGameEffect : Effect<DeclineGameAction>
  {
    private readonly IGameService gameService;

    public DeclineGameEffect(IGameService gameService)
    {
      this.gameService = gameService;
    }

    public override Task HandleAsync(DeclineGameAction action, IDispatcher dispatcher)
    {
      this.gameService.Decline(action.Player.Id, action.GameModel.Game.Id);

      dispatcher.Dispatch(new LoadGameFailureAction(action.ErrorMessage));

      return Task.CompletedTask;
    }
  }
}