namespace Xi.Models.Game
{
  using System.Collections.Generic;

  public class Cannon : Piece
  {
    public const string AsciiChar = "c";

    public Cannon(Color color)
      : base(AsciiChar, color)
    {
    }

    public override List<Cell> Attacking(Cell current, Board board)
    {
      throw new System.NotImplementedException();
    }
  }
}