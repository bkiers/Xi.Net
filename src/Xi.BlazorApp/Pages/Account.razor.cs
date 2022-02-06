namespace Xi.BlazorApp.Pages;

using System.Threading.Tasks;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Xi.BlazorApp.Services;
using Xi.BlazorApp.Stores.Features.Account.Actions.LoadAccount;
using Xi.BlazorApp.Stores.Features.Account.Actions.UpdateAccount;
using Xi.BlazorApp.Stores.States;

public partial class Account
{
  [Inject]
  private IDispatcher Dispatcher { get; set; } = default!;

  [Inject]
  private IState<AccountState> AccountState { get; set; } = default!;

  [Inject]
  private Current Current { get; set; } = default!;

  protected override async Task OnInitializedAsync()
  {
    await base.OnInitializedAsync();

    if (this.AccountState.Value.Player == null)
    {
      this.Dispatcher.Dispatch(new LoadAccountAction(this.Current.LoggedInPlayerId()));
    }
  }

  private void OnShowPossibleMovesToggled(bool enabled)
  {
    var player = this.Current.LoggedInPlayer();
    var settings = player.Settings;

    settings.ShowPossibleMoves = enabled;

    this.Dispatcher.Dispatch(new UpdateAccountSettingsAction(player.Id, settings));
  }
}