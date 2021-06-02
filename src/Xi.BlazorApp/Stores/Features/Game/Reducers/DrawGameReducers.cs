namespace Xi.BlazorApp.Stores.Features.Game.Reducers
{
  using Fluxor;
  using Xi.BlazorApp.Stores.Features.Game.Actions.DrawGame;
  using Xi.BlazorApp.Stores.States;

  public class DrawGameReducers
  {
    [ReducerMethod]
    public static GameState Reduce(GameState state, ProposeDrawAction action)
    {
      return new(true, null, action.GameModel);
    }

    [ReducerMethod]
    public static GameState Reduce(GameState state, ProposeDrawSuccessAction action)
    {
      return new(false, null, action.GameModel);
    }

    [ReducerMethod]
    public static GameState Reduce(GameState state, ProposeDrawFailureAction action)
    {
      return new(false, action.ErrorMessage, action.GameModel);
    }

    [ReducerMethod]
    public static GameState Reduce(GameState state, HandleDrawProposalAction action)
    {
      return new(true, null, action.GameModel);
    }

    [ReducerMethod]
    public static GameState Reduce(GameState state, HandleDrawProposalSuccessAction action)
    {
      return new(false, null, action.GameModel);
    }

    [ReducerMethod]
    public static GameState Reduce(GameState state, HandleDrawProposalFailureAction action)
    {
      return new(false, action.ErrorMessage, action.GameModel);
    }
  }
}