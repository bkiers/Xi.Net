namespace Xi.BlazorApp.Pages
{
  using Fluxor;
  using Microsoft.AspNetCore.Components;
  using Xi.BlazorApp.Stores.Features.Games.Actions.LoadGames;
  using Xi.BlazorApp.Stores.States;

  public partial class Games
  {
    [Inject]
    public IDispatcher Dispatcher { get; set; } = default!;

    [Inject]
    public IState<GamesState> GamesState { get; set; } = default!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    protected override void OnInitialized()
    {
      if (this.GamesState.Value.GameViewModels == null)
      {
        this.Dispatcher.Dispatch(new LoadGamesAction());
      }

      base.OnInitialized();
    }
  }
}