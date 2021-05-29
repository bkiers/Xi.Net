namespace Xi.BlazorApp.Pages
{
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
      if (this.AccountState.Value.Player == null)
      {
        this.Dispatcher.Dispatch(new LoadAccountAction(this.Current.LoggedInPlayerId()));
      }

      await base.OnInitializedAsync();
    }

    private void OnShowPossibleMovesToggled(bool showPossibleMoves)
    {
      this.Dispatcher.Dispatch(new UpdateAccountAction(this.Current.LoggedInPlayerId(), showPossibleMoves));
    }
  }
}