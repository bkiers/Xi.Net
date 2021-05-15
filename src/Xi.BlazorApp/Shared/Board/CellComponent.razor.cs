namespace Xi.BlazorApp.Shared.Board
{
  using System;
  using Fluxor;
  using Microsoft.AspNetCore.Components;
  using Microsoft.Extensions.Logging;
  using Xi.BlazorApp.Models;
  using Xi.BlazorApp.Stores.Features.Game.Actions.ClickBoard;
  using Xi.BlazorApp.Stores.States;
  using Xi.Models.Game;

  public partial class CellComponent
  {
    [Parameter]
    public Cell Cell { get; set; } = default!;

    [Inject]
    public IDispatcher Dispatcher { get; set; } = default!;

    [Inject]
    public IState<GameState> GameState { get; set; } = default!;

    [Inject]
    public ILogger<CellComponent> Logger { get; set; } = default!;

    public string ImageUrl => $"/images/board/{this.Cell.RankIndex + 1}_{this.Cell.FileIndex + 1}.png";

    public void CellClicked()
    {
      if (this.Cell.Occupied)
      {
        this.Logger.LogDebug($"Clicked cell: {this.Cell}");
        this.Dispatcher.Dispatch(new ClickBoardAction(this.Cell, this.GameState.Value.GameViewModel!));
      }
    }

    protected override void OnInitialized()
    {
      base.OnInitialized();
    }
  }
}