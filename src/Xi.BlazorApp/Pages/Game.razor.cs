namespace Xi.BlazorApp.Pages
{
  using Fluxor;
  using Microsoft.AspNetCore.Components;
  using Xi.BlazorApp.Services;
  using Xi.BlazorApp.Stores.Features.Game.Actions.ClickBoard;
  using Xi.BlazorApp.Stores.Features.Game.Actions.LoadGame;
  using Xi.BlazorApp.Stores.States;
  using Xi.Models.Game;

  public partial class Game
  {
    [Parameter]
    public int? GameId { get; set; }

    [Inject]
    public IDispatcher Dispatcher { get; set; } = default!;

    [Inject]
    public IState<GameState> GameState { get; set; } = default!;

    [Inject]
    public Current Current { get; set; } = default!;

    protected override void OnInitialized()
    {
      if (this.GameState.Value.GameModel == null || this.GameState.Value.GameModel.Game.Id != this.GameId)
      {
        this.Dispatcher.Dispatch(new LoadGameAction(this.GameId!.Value));
      }

      base.OnInitialized();
    }

    private bool FlipBoard()
    {
      return this.LoggedInPlayerColor() == Color.Black;
    }

    private Color LoggedInPlayerColor()
    {
      return this.Current.LoggedInPLayerId() == this.GameState.Value.GameModel?.Game.BlackPlayer.Id
        ? Color.Black
        : Color.Red;
    }
  }
}