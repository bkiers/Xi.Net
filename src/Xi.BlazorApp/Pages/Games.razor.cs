namespace Xi.BlazorApp.Pages
{
  using System;
  using System.Threading.Tasks;
  using Fluxor;
  using Microsoft.AspNetCore.Components;
  using Xi.BlazorApp.Stores.Features.Games.Actions.LoadGames;
  using Xi.BlazorApp.Stores.States;
  using Xi.Services;

  public partial class Games
  {
    [Inject]
    public IDispatcher Dispatcher { get; set; } = default!;

    [Inject]
    public IState<GamesState> GamesState { get; set; } = default!;

    [ReducerMethod]
    public static GamesState ReduceLoadGamesAction(GamesState state, LoadGamesAction action)
    {
      return new(true, null, null);
    }

    [ReducerMethod]
    public static GamesState ReduceLoadGamesSuccessAction(GamesState state, LoadGamesSuccessAction action)
    {
      return new(false, null, action.Games);
    }

    [ReducerMethod]
    public static GamesState ReduceLoadGamesFailureAction(GamesState state, LoadGamesFailureAction action)
    {
      return new(false, action.ErrorMessage, null);
    }

    protected override void OnInitialized()
    {
      if (this.GamesState.Value.Games == null)
      {
        this.Dispatcher.Dispatch(new LoadGamesAction());
      }

      base.OnInitialized();
    }
  }
}