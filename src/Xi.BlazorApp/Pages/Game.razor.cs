namespace Xi.BlazorApp.Pages
{
  using System;
  using Fluxor;
  using Microsoft.AspNetCore.Components;
  using Xi.BlazorApp.Stores.Features.Game.Actions.LoadGame;
  using Xi.BlazorApp.Stores.States;

  public partial class Game
  {
    [Parameter]
    public int? GameId { get; set; }

    [Inject]
    public IDispatcher Dispatcher { get; set; } = default!;

    [Inject]
    public IState<GameState> GameState { get; set; } = default!;

    [ReducerMethod]
    public static GameState ReduceLoadGameAction(GameState state, LoadGameAction action)
    {
      return new(true, null, null);
    }

    [ReducerMethod]
    public static GameState ReduceLoadGameSuccessAction(GameState state, LoadGameSuccessAction action)
    {
      return new(false, null, action.Game);
    }

    [ReducerMethod]
    public static GameState ReduceLoadGameFailureAction(GameState state, LoadGameFailureAction action)
    {
      return new(false, action.ErrorMessage, null);
    }

    protected override void OnInitialized()
    {
      if (this.GameState.Value.Game == null || this.GameState.Value.Game.Id != this.GameId)
      {
        this.Dispatcher.Dispatch(new LoadGameAction(this.GameId!.Value));
      }

      base.OnInitialized();
    }
  }
}