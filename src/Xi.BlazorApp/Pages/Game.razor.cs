namespace Xi.BlazorApp.Pages
{
  using System.Threading.Tasks;
  using Fluxor;
  using Microsoft.AspNetCore.Components;
  using Microsoft.AspNetCore.SignalR.Client;
  using Microsoft.Extensions.Logging;
  using Xi.BlazorApp.Hubs;
  using Xi.BlazorApp.Services;
  using Xi.BlazorApp.Shared.Board;
  using Xi.BlazorApp.Stores.Features.Game.Actions.LoadGame;
  using Xi.BlazorApp.Stores.Features.Game.Actions.StartGame;
  using Xi.BlazorApp.Stores.States;
  using Xi.Models.Game;

  public partial class Game
  {
    private HubConnection? hubConnection;

    [Parameter]
    public int? GameId { get; set; }

    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    [Inject]
    private IState<GameState> GameState { get; set; } = default!;

    [Inject]
    private Current Current { get; set; } = default!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
      if (this.GameState.Value.GameModel == null || this.GameState.Value.GameModel.Game.Id != this.GameId)
      {
        this.Dispatcher.Dispatch(new LoadGameAction(this.GameId!.Value));
      }

      this.hubConnection = GamesHub.Connection(this.NavigationManager.BaseUri);

      this.hubConnection.On<int>(EventTypes.MoveMade.ToString(), this.Refresh);

      await this.hubConnection.StartAsync();

      await base.OnInitializedAsync();
    }

    private bool FlipBoard()
    {
      return this.LoggedInPlayerColor() == Color.Black;
    }

    private Color LoggedInPlayerColor()
    {
      return this.Current.LoggedInPlayerId() == this.GameState.Value.GameModel?.Game.BlackPlayer.Id
        ? Color.Black
        : Color.Red;
    }

    private void AcceptGame()
    {
      this.Dispatcher.Dispatch(new AcceptGameAction(this.GameState.Value.GameModel!, this.Current.LoggedInPlayer()));
    }

    private void DeclineGame()
    {
      this.Dispatcher.Dispatch(new DeclineGameAction(this.GameState.Value.GameModel!, this.Current.LoggedInPlayer()));
    }

    private void Refresh(int gameId)
    {
      var user = this.Current.LoggedInPlayer().Name;

      if (this.GameId!.Value == gameId)
      {
        this.Dispatcher.Dispatch(new LoadGameAction(this.GameId!.Value));
      }
    }
  }
}