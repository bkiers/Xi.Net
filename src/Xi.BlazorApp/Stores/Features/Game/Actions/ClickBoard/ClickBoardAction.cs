namespace Xi.BlazorApp.Stores.Features.Game.Actions.ClickBoard;

using Xi.BlazorApp.Models;
using Xi.Models.Game;

public class ClickBoardAction
{
  public ClickBoardAction(Cell clickedCell, GameModel gameModel)
  {
    this.ClickedCell = clickedCell;
    this.GameModel = gameModel;
  }

  public Cell ClickedCell { get; }

  public GameModel GameModel { get; }
}