namespace Xi.Models.Game;

public class MoveResult
{
  public MoveResult(Board board, Piece? capturedPiece)
  {
    this.Board = board;
    this.CapturedPiece = capturedPiece;
  }

  public Board Board { get; }

  public Piece? CapturedPiece { get; }
}