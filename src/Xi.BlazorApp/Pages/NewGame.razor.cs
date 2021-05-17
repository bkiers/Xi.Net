namespace Xi.BlazorApp.Pages
{
  using Fluxor;
  using Microsoft.AspNetCore.Components;
  using Microsoft.Extensions.Logging;
  using Xi.BlazorApp.Models;
  using Xi.BlazorApp.Services;
  using Xi.BlazorApp.Stores.Features.NewGame.Actions.CreateNewGame;

  public partial class NewGame
  {
    [Inject]
    public IDispatcher Dispatcher { get; set; } = default!;

    [Inject]
    public IPlayerService PlayerService { get; set; } = default!;

    private NewGameModel NewGameModel { get; } = new();

    private void HandleSubmit()
    {
      this.Dispatcher.Dispatch(new CreateNewGameAction());
    }
  }
}