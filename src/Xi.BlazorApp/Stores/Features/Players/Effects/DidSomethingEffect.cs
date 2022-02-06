namespace Xi.BlazorApp.Stores.Features.Players.Effects;

using System.Threading.Tasks;
using Fluxor;
using Xi.BlazorApp.Services;
using Xi.BlazorApp.Stores.Features.Players.Actions;

public class DidSomethingEffect : Effect<DidSomethingAction>
{
  private readonly IPlayerService playerService;

  public DidSomethingEffect(IPlayerService playerService)
  {
    this.playerService = playerService;
  }

  public override Task HandleAsync(DidSomethingAction action, IDispatcher dispatcher)
  {
    this.playerService.DidSomething(action.PLayerId);

    return Task.CompletedTask;
  }
}