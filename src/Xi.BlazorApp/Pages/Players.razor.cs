namespace Xi.BlazorApp.Pages
{
  using Fluxor;
  using Microsoft.AspNetCore.Components;
  using Xi.BlazorApp.Services;
  using Xi.BlazorApp.Stores.Features.Players.Actions;
  using Xi.BlazorApp.Stores.States;

  public partial class Players
  {
    [Inject]
    private IState<PlayersState> PlayersState { get; set; } = default!;

    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    [Inject]
    private Current Current { get; set; } = default!;

    protected override void OnInitialized()
    {
      base.OnInitialized();

      this.Dispatcher.Dispatch(new DidSomethingAction(this.Current.PossibleLoggedInPlayerId()));
      this.Dispatcher.Dispatch(new GetPlayersAction());
    }
  }
}