namespace Xi.BlazorApp.Stores.Features.Account.Effects;

using System.Threading.Tasks;
using Fluxor;
using Xi.BlazorApp.Services;
using Xi.BlazorApp.Stores.Features.Account.Actions.UpdateAccount;

public class UpdateAccountSettingsEffect : Effect<UpdateAccountSettingsAction>
{
  private readonly IPlayerService playerService;

  public UpdateAccountSettingsEffect(IPlayerService playerService)
  {
    this.playerService = playerService;
  }

  public override Task HandleAsync(UpdateAccountSettingsAction settingsAction, IDispatcher dispatcher)
  {
    var player = this.playerService.Update(settingsAction.PlayerId, settingsAction.Settings);

    dispatcher.Dispatch(new UpdateAccountSuccessAction(player));

    return Task.CompletedTask;
  }
}