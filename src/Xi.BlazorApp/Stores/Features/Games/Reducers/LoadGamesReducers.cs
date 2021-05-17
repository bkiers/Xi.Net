namespace Xi.BlazorApp.Stores.Features.Games.Reducers
{
  using Fluxor;
  using Xi.BlazorApp.Stores.Features.Games.Actions.LoadGames;
  using Xi.BlazorApp.Stores.States;

  public class LoadGamesReducers
  {
    [ReducerMethod]
    public static GamesState Reduce(GamesState state, LoadGamesAction action)
    {
      return new(true, null, null);
    }

    [ReducerMethod]
    public static GamesState Reduce(GamesState state, LoadGamesSuccessAction action)
    {
      return new(false, null, action.GameViewModels);
    }

    [ReducerMethod]
    public static GamesState Reduce(GamesState state, LoadGamesFailureAction action)
    {
      return new(false, action.ErrorMessage, null);
    }
  }
}