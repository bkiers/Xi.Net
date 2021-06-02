namespace Xi.BlazorApp.Shared.Board
{
  using System;
  using Microsoft.AspNetCore.Components;
  using Xi.BlazorApp.Models;
  using Xi.Models.Game;

  public partial class PlayerComponent
  {
    [Parameter]
    public bool IsTop { get; set; }

    [Parameter]
    public bool FlipBoard { get; set; }

    [Parameter]
    public GameModel GameModel { get; set; } = default!;

    private Player Player => this.CurrentColor == Color.Red ?
      this.GameModel.Game.RedPlayer :
      this.GameModel.Game.BlackPlayer;

    private bool ShowClock => this.GameModel.Game.ClockRunsOutAt.HasValue &&
                              !this.GameModel.Game.IsOver() &&
                              this.GameModel.ActualTurnPlayerColor == this.CurrentColor;

    private Color CurrentColor => this.IsTop ?
      (this.FlipBoard ? Color.Red : Color.Black) :
      (this.FlipBoard ? Color.Black : Color.Red);
  }
}