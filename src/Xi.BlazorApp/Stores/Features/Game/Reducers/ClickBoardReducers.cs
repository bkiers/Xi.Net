namespace Xi.BlazorApp.Stores.Features.Game.Reducers
{
  using Fluxor;
  using Xi.BlazorApp.Stores.Features.Game.Actions.ClickBoard;
  using Xi.BlazorApp.Stores.States;

  public class ClickBoardReducers
  {
    [ReducerMethod]
    public static GameState Reduce(GameState state, ClickBoardAction action)
    {
      return new(false, null, action.GameModel);
    }

    [ReducerMethod]
    public static GameState Reduce(GameState state, ClickBoardValidAction action)
    {
      return new(false, null, action.GameModel);
    }

    [ReducerMethod]
    public static GameState Reduce(GameState state, ClickBoardInvalidAction action)
    {
      return new(false, action.ErrorMessage, action.GameModel);
    }
  }
}