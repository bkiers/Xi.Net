namespace Xi.BlazorApp.Stores.Features.Account.Effects;

using System.Threading.Tasks;
using Fluxor;
using Xi.BlazorApp.Services;
using Xi.BlazorApp.Stores.Features.Account.Actions.LoadAccount;

public class LoadAccountEffect : Effect<LoadAccountAction>
{
  private readonly IPlayerService playerService;

  public LoadAccountEffect(IPlayerService playerService)
  {
    this.playerService = playerService;
  }

  public override Task HandleAsync(LoadAccountAction action, IDispatcher dispatcher)
  {
    var player = this.playerService.FindById(action.PlayerId);

    dispatcher.Dispatch(new LoadAccountSuccessAction(player));

    return Task.CompletedTask;
  }
}