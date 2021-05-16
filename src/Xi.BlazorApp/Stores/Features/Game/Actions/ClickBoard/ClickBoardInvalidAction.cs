namespace Xi.BlazorApp.Stores.Features.Game.Actions.ClickBoard
{
  using Xi.BlazorApp.Models;
  using Xi.Models.Game;

  public class ClickBoardInvalidAction
  {
    public ClickBoardInvalidAction(Cell clickedCell, GameViewModel gameViewModel, string errorMessage)
    {
      this.ClickedCell = clickedCell;
      this.GameViewModel = gameViewModel;
      this.ErrorMessage = errorMessage;
    }

    public Cell ClickedCell { get; }

    public GameViewModel GameViewModel { get; }

    public string ErrorMessage { get; }
  }
}