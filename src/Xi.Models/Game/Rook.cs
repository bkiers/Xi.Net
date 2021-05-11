namespace Xi.Models.Game
{
  using System.Collections.Generic;

  public class Rook : Piece
  {
    public const string AsciiChar = "r";

    public Rook(Color color)
      : base(AsciiChar, color)
    {
    }

    public override List<Cell> Attacking(Cell current, Board board)
    {
      throw new System.NotImplementedException();
    }
  }
}