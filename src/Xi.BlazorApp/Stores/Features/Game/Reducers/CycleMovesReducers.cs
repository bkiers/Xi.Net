namespace Xi.BlazorApp.Stores.Features.Game.Reducers
{
  using System;
  using System.Linq;
  using Fluxor;
  using Xi.BlazorApp.Models;
  using Xi.BlazorApp.Stores.Features.Game.Actions.CycleMoves;
  using Xi.BlazorApp.Stores.States;

  public class CycleMovesReducers
  {
    [ReducerMethod]
    public static GameState Reduce(GameState state, PreviousMoveAction action)
    {
      var model = new GameModel(
        action.GameModel.Game,
        Math.Max(0, action.GameModel.CurrentMoveIndex - 1),
        action.GameModel.FirstClick);

      return new GameState(false, null, model);
    }

    [ReducerMethod]
    public static GameState Reduce(GameState state, NextMoveAction action)
    {
      var model = new GameModel(
        action.GameModel.Game,
        Math.Min(action.GameModel.Game.Moves.Count - 1, action.GameModel.CurrentMoveIndex + 1),
        action.GameModel.FirstClick);

      return new GameState(false, null, model);
    }

    [ReducerMethod]
    public static GameState Reduce(GameState state, FirstMoveAction action)
    {
      var model = new GameModel(
        action.GameModel.Game,
        action.GameModel.Game.Moves.Any() ? 0 : -1,
        action.GameModel.FirstClick);

      return new GameState(false, null, model);
    }

    [ReducerMethod]
    public static GameState Reduce(GameState state, LastMoveAction action)
    {
      var model = new GameModel(
        action.GameModel.Game,
        action.GameModel.Game.Moves.Any() ? action.GameModel.Game.Moves.Count - 1 : -1,
        action.GameModel.FirstClick);

      return new GameState(false, null, model);
    }
  }
}