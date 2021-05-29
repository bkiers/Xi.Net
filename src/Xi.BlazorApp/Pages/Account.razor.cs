namespace Xi.BlazorApp.Pages
{
  using System.Threading.Tasks;
  using Fluxor;
  using Microsoft.AspNetCore.Components;
  using Xi.BlazorApp.Models;
  using Xi.BlazorApp.Services;
  using Xi.BlazorApp.Stores.Features.Account.Actions.LoadAccount;
  using Xi.BlazorApp.Stores.States;

  public partial class Account
  {
    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    [Inject]
    private IPlayerService PlayerService { get; set; } = default!;

    [Inject]
    private IState<AccountState> AccountState { get; set; } = default!;

    [Inject]
    private Current Current { get; set; } = default!;

    private UpdateAccountModel UpdateAccountModel { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
      if (this.AccountState.Value.Player == null)
      {
        this.Dispatcher.Dispatch(new LoadAccountAction(this.Current.LoggedInPlayerId()));
      }

      this.AccountState.StateChanged += (sender, state) =>
      {
        if (state.Player is null)
        {
          return;
        }

        this.UpdateAccountModel = new UpdateAccountModel
        {
          ShowPossibleMoves = state.Player.ShowPossibleMoves,
        };

        this.StateHasChanged();
      };

      await base.OnInitializedAsync();
    }

    private void OnShowPossibleMovesToggled()
    {
    }
  }
}