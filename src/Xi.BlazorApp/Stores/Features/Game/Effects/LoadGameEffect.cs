namespace Xi.BlazorApp.Stores.Features.Game.Effects
{
  using System.Threading.Tasks;
  using Fluxor;
  using Xi.BlazorApp.Services;
  using Xi.BlazorApp.Stores.Features.Game.Actions.LoadGame;

  public class LoadGameEffect : Effect<LoadGameAction>
  {
    private readonly IGameService gameService;

    public LoadGameEffect(IGameService gameService)
    {
      this.gameService = gameService;
    }

    public override Task HandleAsync(LoadGameAction action, IDispatcher dispatcher)
    {
      var gameViewModel = this.gameService.Game(action.GameId);

      if (gameViewModel == null)
      {
        dispatcher.Dispatch(new LoadGameFailureAction($"Could not find a game with id: '{action.GameId}'"));
      }
      else
      {
        dispatcher.Dispatch(new LoadGameSuccessAction(gameViewModel!));
      }

      return Task.CompletedTask;
    }
  }
}