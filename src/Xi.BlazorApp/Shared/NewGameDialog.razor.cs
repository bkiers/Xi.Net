namespace Xi.BlazorApp.Shared
{
  using System.Collections.Generic;
  using System.Linq;
  using Fluxor;
  using Microsoft.AspNetCore.Components;
  using MudBlazor;
  using Xi.BlazorApp.Models;
  using Xi.BlazorApp.Services;
  using Xi.BlazorApp.Stores.Features.NewGame.Actions.CreateNewGame;
  using Xi.Models.Game;
  using Color = Xi.Models.Game.Color;

  public partial class NewGameDialog
  {
    [Inject]
    private ISnackbar Snackbar { get; set; } = default!;

    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    private IPlayerService PlayerService { get; set; } = default!;

    [Inject]
    private Current Current { get; set; } = default!;

    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = default!;

    private NewGameModel NewGameModel { get; set; } = new();

    private IEnumerable<Player> PossibleOpponents =>
      this.PlayerService.AllPlayersExcept(this.Current.LoggedInPlayerId());

    protected override void OnInitialized()
    {
      base.OnInitialized();

      this.NewGameModel = new NewGameModel
      {
        PlayingWithColor = Color.Black,
        OpponentPlayerId = this.PossibleOpponents.FirstOrDefault()?.Id ?? -1,
        DaysPerMove = 4,
      };
    }

    private void Cancel()
    {
      this.MudDialog.Cancel();
    }

    private void CreateGame()
    {
      this.Dispatcher.Dispatch(new CreateNewGameAction(
        this.Current.LoggedInPlayerId(),
        this.NewGameModel.PlayingWithColor,
        this.NewGameModel.OpponentPlayerId,
        this.NewGameModel.DaysPerMove));

      var opponent = this.PlayerService.FindById(this.NewGameModel.OpponentPlayerId);

      this.Snackbar.Add($"Game created and {opponent.Name} received an email.", Severity.Success);

      this.NavigationManager.NavigateTo($"/games");

      this.MudDialog.Close();
    }
  }
}