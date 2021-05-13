namespace Xi.BlazorApp.Pages
{
  using System.Linq;
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

    [ReducerMethod]
    public static GamesState ReduceLoadGamesAction(GamesState state, LoadGamesAction action) =>
      new GamesState(true, null, null);

    [ReducerMethod]
    public static GamesState ReduceLoadGamesSuccessAction(GamesState state, LoadGamesSuccessAction action) =>
      new GamesState(false, null, action.Games);

    [ReducerMethod]
    public static GamesState ReduceLoadGamesFailureAction(GamesState state, LoadGamesFailureAction action) =>
      new GamesState(false, action.ErrorMessage, null);

    protected override void OnInitialized()
    {
      if (this.GamesState.Value.Games?.Any() != true)
      {
        this.Dispatcher.Dispatch(new LoadGamesAction());
      }

      base.OnInitialized();
    }
  }
}