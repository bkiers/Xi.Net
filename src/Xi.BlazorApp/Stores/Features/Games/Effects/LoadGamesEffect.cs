namespace Xi.BlazorApp.Stores.Features.Games.Effects
{
  using System.Threading.Tasks;
  using Fluxor;
  using Xi.BlazorApp.Services;
  using Xi.BlazorApp.Stores.Features.Games.Actions.LoadGames;

  public class LoadGamesEffect : Effect<LoadGamesAction>
  {
    private readonly IGameService gameService;

    public LoadGamesEffect(IGameService gameService)
    {
      this.gameService = gameService;
    }

    public override Task HandleAsync(LoadGamesAction action, IDispatcher dispatcher)
    {
      var games = this.gameService.Games();

      dispatcher.Dispatch(new LoadGamesSuccessAction(games));

      return Task.CompletedTask;
    }
  }
}