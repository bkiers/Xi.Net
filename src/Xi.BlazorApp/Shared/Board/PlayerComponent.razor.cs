namespace Xi.BlazorApp.Shared.Board;

using System;
using System.Threading.Tasks;
using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Xi.BlazorApp.Models;
using Xi.BlazorApp.Services;
using Xi.BlazorApp.Stores.Features.ExtendClock.Actions;
using Xi.BlazorApp.Stores.Features.Game.Actions.EndGame;
using Xi.BlazorApp.Stores.States;
using Xi.Models.Game;
using Color = Xi.Models.Game.Color;

public partial class PlayerComponent
{
  private bool boughtExtraTime = false;

  [Parameter]
  public bool IsTop { get; set; }

  [Parameter]
  public bool FlipBoard { get; set; }

  [Parameter]
  public GameModel GameModel { get; set; } = default!;

  [Inject]
  private IState<ExtendClockState> ExtendClockState { get; set; } = default!;

  [Inject]
  private IDispatcher Dispatcher { get; set; } = default!;

  [Inject]
  private IDialogService DialogService { get; set; } = default!;

  [Inject]
  private Current Current { get; set; } = default!;

  private Player Player => this.CurrentColor == Color.Red ?
    this.GameModel.Game.RedPlayer :
    this.GameModel.Game.BlackPlayer;

  private bool ShowClock => this.GameModel.Game.ClockRunsOutAt.HasValue &&
                            !this.GameModel.Game.IsOver() &&
                            this.GameModel.ActualTurnPlayerColor == this.CurrentColor;

  private Color CurrentColor => this.IsTop ?
    (this.FlipBoard ? Color.Red : Color.Black) :
    (this.FlipBoard ? Color.Black : Color.Red);

  protected override void OnInitialized()
  {
    base.OnInitialized();

    this.Dispatcher.Dispatch(new GetExtendClockAction(this.Current.LoggedInPlayerId(), this.GameModel.Game.Id));
  }

  private async Task Extend()
  {
    var parameters = new DialogParameters
    {
      [nameof(YesNoDialog.Title)] = "More time!",
      [nameof(YesNoDialog.Content)] = "Buy an extra 12 hours time on your clock for -1 ELO point (which will go to your opponent)?",
      [nameof(YesNoDialog.InverseButtonColors)] = true,
    };

    var dialog = this.DialogService.Show<YesNoDialog>(string.Empty, parameters);
    var result = await dialog.Result;

    if (!result.Cancelled)
    {
      this.boughtExtraTime = true;

      this.Dispatcher.Dispatch(new BuyExtraTimeAction(this.Current.LoggedInPlayerId(), this.GameModel.Game.Id));
    }
  }

  private bool ShowExtendClockButton()
  {
    return !this.boughtExtraTime
           && this.ExtendClockState.Value.IsPossible == true
           && this.GameModel.Game.TurnPlayer().Id == this.Current.LoggedInPlayerId()
           && this.Player.Id == this.Current.LoggedInPlayerId();
  }

  private string PotentialPoints()
  {
    var game = this.GameModel.Game;

    if (!game.IsOver())
    {
      return string.Empty;
    }

    var change = this.CurrentColor == Color.Red
      ? game.EloRatingChangeRed!.Value
      : game.EloRatingChangeBlack!.Value;

    return change > 0 ? $"+{change}" : $"{change}";
  }
}