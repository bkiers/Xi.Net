namespace Xi.BlazorApp.Stores.Features.NewGame.Reducers
{
  using Fluxor;
  using Xi.BlazorApp.Stores.Features.NewGame.Actions.CreateNewGame;
  using Xi.BlazorApp.Stores.States;

  public class NewGameReducers
  {
    [ReducerMethod]
    public static NewGameState Reduce(NewGameState state, CreateNewGameAction action)
    {
      return new(true, null, null);
    }

    [ReducerMethod]
    public static NewGameState Reduce(NewGameState state, CreateNewGameSuccessAction action)
    {
      return new(false, null, action.GameModel);
    }

    [ReducerMethod]
    public static NewGameState Reduce(NewGameState state, CreateNewGameFailureAction action)
    {
      return new(false, action.ErrorMessage, null);
    }
  }
}