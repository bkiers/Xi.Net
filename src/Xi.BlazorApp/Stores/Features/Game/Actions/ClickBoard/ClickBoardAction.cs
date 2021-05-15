namespace Xi.BlazorApp.Stores.Features.Game.Actions.ClickBoard
{
  using Xi.BlazorApp.Models;
  using Xi.Models.Game;

  public class ClickBoardAction
  {
    public ClickBoardAction(Cell clickedCell, GameViewModel gameViewModel)
    {
      this.ClickedCell = clickedCell;
      this.GameViewModel = gameViewModel;
    }

    public Cell ClickedCell { get; }

    public GameViewModel GameViewModel { get; }
  }
}