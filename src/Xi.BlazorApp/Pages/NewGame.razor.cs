namespace Xi.BlazorApp.Pages
{
  using Fluxor;
  using Microsoft.AspNetCore.Components;
  using Xi.BlazorApp.Models;
  using Xi.BlazorApp.Services;
  using Xi.BlazorApp.Stores.Features.LoggedInPlayer.Actions.ReadLoggedInPlayerFromCookie;
  using Xi.BlazorApp.Stores.Features.NewGame.Actions.CreateNewGame;
  using Xi.BlazorApp.Stores.States;

  public partial class NewGame
  {
    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    [Inject]
    private IPlayerService PlayerService { get; set; } = default!;

    [Inject]
    private IState<NewGameState> NewGameState { get; set; } = default!;

    [Inject]
    private IState<LoggedInPlayerState> LoggedInPlayerState { get; set; } = default!;

    private NewGameModel NewGameModel { get; } = new();

    protected override void OnInitialized()
    {
      if (this.LoggedInPlayerState.Value.Player == null)
      {
        this.Dispatcher.Dispatch(new ReadLoggedInPlayerFromCookieAction());
      }

      base.OnInitialized();
    }

    private void HandleSubmit()
    {
      this.Dispatcher.Dispatch(new CreateNewGameAction(
        this.LoggedInPlayerState.PlayerId(),
        this.NewGameModel.PlayingWithColor,
        this.NewGameModel.OpponentPlayerId,
        this.NewGameModel.DaysPerMove));
    }
  }
}