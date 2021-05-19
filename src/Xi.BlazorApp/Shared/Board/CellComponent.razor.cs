namespace Xi.BlazorApp.Shared.Board
{
  using System;
  using System.Threading.Tasks;
  using Fluxor;
  using Microsoft.AspNetCore.Components;
  using Microsoft.AspNetCore.Components.Authorization;
  using Microsoft.Extensions.Logging;
  using Xi.BlazorApp.Models;
  using Xi.BlazorApp.Stores.Features.Game.Actions.ClickBoard;
  using Xi.BlazorApp.Stores.States;
  using Xi.Models.Game;

  public partial class CellComponent
  {
    [Parameter]
    public Cell Cell { get; set; } = default!;

    [Parameter]
    public bool ShouldHighlight { get; set; }

    [Parameter]
    public bool FlipBoard { get; set; }

    [Inject]
    public IDispatcher Dispatcher { get; set; } = default!;

    [Inject]
    public IState<GameState> GameState { get; set; } = default!;

    [Inject]
    public ILogger<CellComponent> Logger { get; set; } = default!;

    public string ImageUrl => this.FlipBoard
      ? $"/images/board/{10 - this.Cell.RankIndex}_{this.Cell.FileIndex + 1}.png"
      : $"/images/board/{this.Cell.RankIndex + 1}_{this.Cell.FileIndex + 1}.png";

    public void CellClicked()
    {
      // Only dispatch an action when the cell is occupied, or the user already made a first click.
      if (this.Cell.Occupied || this.GameState.Value.GameModel!.FirstClick != null)
      {
        this.Logger.LogDebug($"Clicked cell: {this.Cell}");
        this.Dispatcher.Dispatch(new ClickBoardAction(this.Cell, this.GameState.Value.GameModel!));
      }
    }
  }
}