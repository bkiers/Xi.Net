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