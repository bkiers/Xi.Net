namespace Xi.BlazorApp.Stores.Features.LoggedInPlayer.Reducers
{
  using Fluxor;
  using Xi.BlazorApp.Stores.Features.LoggedInPlayer.Actions.ReadLoggedInPlayerFromCookie;
  using Xi.BlazorApp.Stores.States;

  public class ReadLoggedInPlayerFromCookieReducers
  {
    [ReducerMethod]
    public static LoggedInPlayerState Reduce(LoggedInPlayerState state, ReadLoggedInPlayerFromCookieAction action)
    {
      return new(true,  null);
    }

    [ReducerMethod]
    public static LoggedInPlayerState Reduce(LoggedInPlayerState state, ReadLoggedInPlayerFromCookieSuccessAction action)
    {
      return new(false, action.Player);
    }
  }
}