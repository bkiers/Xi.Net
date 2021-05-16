namespace Xi.BlazorApp.Stores.Features.Game.Reducers
{
  using Fluxor;
  using Xi.BlazorApp.Stores.Features.Game.Actions.LoadGame;
  using Xi.BlazorApp.Stores.States;

  public class LoadGameReducers
  {
    [ReducerMethod]
    public static GameState ReduceLoadGameAction(GameState state, LoadGameAction action)
    {
      return new(true, null, null);
    }

    [ReducerMethod]
    public static GameState ReduceLoadGameSuccessAction(GameState state, LoadGameSuccessAction action)
    {
      return new(false, null, action.GameViewModel);
    }

    [ReducerMethod]
    public static GameState ReduceLoadGameFailureAction(GameState state, LoadGameFailureAction action)
    {
      return new(false, action.ErrorMessage, null);
    }
  }
}