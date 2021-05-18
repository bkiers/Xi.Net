namespace Xi.BlazorApp.Pages
{
  using Fluxor;
  using Microsoft.AspNetCore.Components;
  using Xi.BlazorApp.Stores.Features.LoggedInPlayer.Actions.ReadLoggedInPlayerFromCookie;
  using Xi.BlazorApp.Stores.States;

  public partial class Index
  {
    [Inject]
    public IDispatcher Dispatcher { get; set; } = default!;

    [Inject]
    public IState<LoggedInPlayerState> LoggedInPlayerState { get; set; } = default!;

    protected override void OnInitialized()
    {
      if (this.LoggedInPlayerState.Value.Player == null)
      {
        this.Dispatcher.Dispatch(new ReadLoggedInPlayerFromCookieAction());
      }

      base.OnInitialized();
    }
  }
}