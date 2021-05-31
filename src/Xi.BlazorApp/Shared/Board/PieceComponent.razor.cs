namespace Xi.BlazorApp.Shared.Board
{
  using Fluxor;
  using Microsoft.AspNetCore.Components;
  using Xi.BlazorApp.Stores.States;
  using Xi.Models.Game;

  public partial class PieceComponent
  {
    private const int PieceSize = 50;

    [Parameter]
    public Piece? Piece { get; set; }

    [Parameter]
    public bool IsLastMovedPiece { get; set; }

    [Inject]
    private IState<GameState> GameState { get; set; } = default!;

    private string PieceUrl => this.Piece == null ?
      string.Empty :
      $"/images/pieces/{this.Piece!.AsciiNotation.ToLower()}{this.Piece!.Color.ToString().ToLower()[0]}.svg";

    private int PreviousPixelPosition(bool isFile)
    {
      var (fromCell, toCell) = this.GameState.Value.GameModel!.GetCurrentMoveCells();

      var delta = isFile ?
        fromCell.FileIndex - toCell.FileIndex :
        fromCell.RankIndex - toCell.RankIndex;

      return delta * PieceSize;
    }
  }
}