namespace Xi.BlazorApp.Stores.Features.Game.Reducers
{
  using Fluxor;
  using Xi.BlazorApp.Stores.Features.Game.Actions.StartGame;
  using Xi.BlazorApp.Stores.States;

  public class StartGameReducers
  {
    [ReducerMethod]
    public static GameState Reduce(GameState state, AcceptGameAction action)
    {
      return new(false, null, action.GameModel);
    }

    [ReducerMethod]
    public static GameState Reduce(GameState state, DeclineGameAction action)
    {
      return new(false, null, action.GameModel);
    }
  }
}