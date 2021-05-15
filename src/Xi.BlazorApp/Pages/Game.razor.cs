namespace Xi.BlazorApp.Pages
{
  using Fluxor;
  using Microsoft.AspNetCore.Components;
  using Xi.BlazorApp.Stores.Features.Game.Actions.ClickBoard;
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
      return new(false, null, action.GameViewModel);
    }

    [ReducerMethod]
    public static GameState ReduceLoadGameFailureAction(GameState state, LoadGameFailureAction action)
    {
      return new(false, action.ErrorMessage, null);
    }

    [ReducerMethod]
    public static GameState ReduceClickBoardAction(GameState state, ClickBoardAction action)
    {
      return new(false, null, action.GameViewModel);
    }

    protected override void OnInitialized()
    {
      if (this.GameState.Value.GameViewModel == null || this.GameState.Value.GameViewModel.Game.Id != this.GameId)
      {
        this.Dispatcher.Dispatch(new LoadGameAction(this.GameId!.Value));
      }

      base.OnInitialized();
    }
  }
}