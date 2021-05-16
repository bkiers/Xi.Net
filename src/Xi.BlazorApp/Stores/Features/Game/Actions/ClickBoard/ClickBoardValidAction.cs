namespace Xi.BlazorApp.Stores.Features.Game.Actions.ClickBoard
{
  using Xi.BlazorApp.Models;
  using Xi.Models.Game;

  public class ClickBoardValidAction
  {
    public ClickBoardValidAction(Cell clickedCell, GameViewModel gameViewModel)
    {
      this.ClickedCell = clickedCell;
      this.GameViewModel = gameViewModel;
    }

    public Cell ClickedCell { get; }

    public GameViewModel GameViewModel { get; }
  }
}