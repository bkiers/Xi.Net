namespace Xi.BlazorApp.Stores.Features.Games.Reducers
{
  using Fluxor;
  using Xi.BlazorApp.Stores.Features.Games.Actions.LoadGames;
  using Xi.BlazorApp.Stores.States;

  public class LoadGamesActionsReducer
  {
    [ReducerMethod]
    public static GamesState ReduceLoadGamesAction(GamesState state, LoadGamesAction action) =>
      new GamesState(true, null, null);

    [ReducerMethod]
    public static GamesState ReduceLoadGamesSuccessAction(GamesState state, LoadGamesSuccessAction action) =>
      new GamesState(false, null, action.Games);

    [ReducerMethod]
    public static GamesState ReduceLoadGamesFailureAction(GamesState state, LoadGamesFailureAction action) =>
      new GamesState(false, action.ErrorMessage, null);
  }
}
