namespace Xi.BlazorApp.Stores.Features.Players.Effects
{
  using System.Linq;
  using System.Threading.Tasks;
  using Fluxor;
  using Xi.BlazorApp.Services;
  using Xi.BlazorApp.Stores.Features.Players.Actions;

  public class GetPlayersEffect : Effect<GetPlayersAction>
  {
    private readonly IPlayerService playerService;

    public GetPlayersEffect(IPlayerService playerService)
    {
      this.playerService = playerService;
    }

    public override Task HandleAsync(GetPlayersAction action, IDispatcher dispatcher)
    {
      var players = this.playerService
        .AllPlayers()
        .OrderByDescending(p => p.EloRating)
        .ToList();

      dispatcher.Dispatch(new GetPlayersResult(players));

      return Task.CompletedTask;
    }
  }
}