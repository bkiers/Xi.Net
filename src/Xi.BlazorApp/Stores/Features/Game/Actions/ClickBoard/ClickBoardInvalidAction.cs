namespace Xi.BlazorApp.Stores.Features.Game.Actions.ClickBoard
{
  using Xi.BlazorApp.Models;
  using Xi.Models.Game;

  public class ClickBoardInvalidAction
  {
    public ClickBoardInvalidAction(Cell clickedCell, GameModel gameModel, string errorMessage)
    {
      this.ClickedCell = clickedCell;
      this.GameModel = gameModel;
      this.ErrorMessage = errorMessage;
    }

    public Cell ClickedCell { get; }

    public GameModel GameModel { get; }

    public string ErrorMessage { get; }
  }
}