namespace Xi.BlazorApp.Stores.Features.Game.Reducers
{
  using System;
  using System.Linq;
  using Fluxor;
  using Xi.BlazorApp.Models;
  using Xi.BlazorApp.Stores.Features.Game.Actions.Moves;
  using Xi.BlazorApp.Stores.States;

  public class MovesReducers
  {
    [ReducerMethod]
    public static GameState Reduce(GameState state, JumpToMoveAction action)
    {
      var model = new GameModel(
        action.GameModel.Game,
        action.GameModel.ActualTurnPlayerColor,
        action.Index,
        action.GameModel.FirstClick);

      return new GameState(false, null, model);
    }

    [ReducerMethod]
    public static GameState Reduce(GameState state, PreviousMoveAction action)
    {
      var model = new GameModel(
        action.GameModel.Game,
        action.GameModel.ActualTurnPlayerColor,
        Math.Max(0, action.GameModel.CurrentMoveIndex - 1),
        action.GameModel.FirstClick);

      return new GameState(false, null, model);
    }

    [ReducerMethod]
    public static GameState Reduce(GameState state, NextMoveAction action)
    {
      var model = new GameModel(
        action.GameModel.Game,
        action.GameModel.ActualTurnPlayerColor,
        Math.Min(action.GameModel.Game.Moves.Count - 1, action.GameModel.CurrentMoveIndex + 1),
        action.GameModel.FirstClick);

      return new GameState(false, null, model);
    }

    [ReducerMethod]
    public static GameState Reduce(GameState state, FirstMoveAction action)
    {
      var model = new GameModel(
        action.GameModel.Game,
        action.GameModel.ActualTurnPlayerColor,
        action.GameModel.Game.Moves.Any() ? 0 : -1,
        action.GameModel.FirstClick);

      return new GameState(false, null, model);
    }

    [ReducerMethod]
    public static GameState Reduce(GameState state, LastMoveAction action)
    {
      var model = new GameModel(
        action.GameModel.Game,
        action.GameModel.ActualTurnPlayerColor,
        action.GameModel.Game.Moves.Any() ? action.GameModel.Game.Moves.Count - 1 : -1,
        action.GameModel.FirstClick);

      return new GameState(false, null, model);
    }

    [ReducerMethod]
    public static GameState Reduce(GameState state, RemoveMovesAction action)
    {
      var model = new GameModel(
        action.GameModel.Game.RemoveMovesFrom(action.FromIndex),
        action.GameModel.ActualTurnPlayerColor,
        action.FromIndex - 1,
        null);

      return new GameState(false, null, model);
    }

    [ReducerMethod]
    public static GameState Reduce(GameState state, ConfirmMoveAction action)
    {
      return new(false, null, action.GameModel);
    }

    [ReducerMethod]
    public static GameState Reduce(GameState state, ConfirmMoveSuccessAction action)
    {
      return new(false, null, action.GameModel);
    }

    [ReducerMethod]
    public static GameState Reduce(GameState state, ConfirmMoveFailureAction action)
    {
      return new(false, action.ErrorMessage, action.GameModel);
    }
  }
}